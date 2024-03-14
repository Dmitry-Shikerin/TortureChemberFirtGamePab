using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.Presentation.Containers.GamePoints;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Voids;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public abstract class LoadServiceBase : ILoadService
    {
        private readonly BackgroundMusicViewFactory _backgroundMusicViewFactory;
        private readonly FoodPickUpPointsViewFactory _foodPickUpPointsViewFactory;
        private readonly GameplayFormServiceFactory _gameplayFormServiceFactory;
        private readonly HUD _hud;

        private readonly ImageUIFactory _imageUIFactory;
        private readonly ItemProvider<IItem> _itemProvider;
        private readonly PlayerCameraView _playerCameraView;
        private readonly IPlayerProviderSetter _playerProviderSetter;
        private readonly PlayerViewFactory _playerViewFactory;
        private readonly RootGamePoints _rootGamePoints;
        private readonly Setting _setting;
        private readonly TavernMoodViewFactory _tavernMoodViewFactory;
        private readonly ITavernProviderSetter _tavernProviderSetter;
        private readonly TextUIFactory _textUIFactory;
        private readonly IUpgradeProviderSetter _upgradeProviderSetter;
        private readonly VisitorPointsRepositoryFactory _visitorPointsRepositoryFactory;

        private Player _player;
        private PlayerUpgrade _playerUpgrade;
        private Tavern _tavern;

        protected LoadServiceBase(
            BackgroundMusicViewFactory backgroundMusicViewFactory,
            Setting setting,
            FoodPickUpPointsViewFactory foodPickUpPointsViewFactory,
            PlayerCameraView playerCameraView,
            RootGamePoints rootGamePoints,
            IPlayerProviderSetter playerProviderSetter,
            ITavernProviderSetter tavernProviderSetter,
            IUpgradeProviderSetter upgradeProviderSetter,
            TavernMoodViewFactory tavernMoodViewFactory,
            HUD hud,
            ItemProvider<IItem> itemProvider,
            TextUIFactory textUIFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            ImageUIFactory imageUIFactory,
            GameplayFormServiceFactory gameplayFormServiceFactory,
            VisitorPointsRepositoryFactory visitorPointsRepositoryFactory,
            PlayerViewFactory playerViewFactory)
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _rootGamePoints = rootGamePoints ? rootGamePoints : throw new ArgumentNullException(nameof(rootGamePoints));
            _playerCameraView = playerCameraView
                ? playerCameraView
                : throw new ArgumentNullException(nameof(playerCameraView));
            _backgroundMusicViewFactory = backgroundMusicViewFactory ??
                                          throw new ArgumentNullException(nameof(backgroundMusicViewFactory));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _foodPickUpPointsViewFactory = foodPickUpPointsViewFactory ??
                                           throw new ArgumentNullException(nameof(foodPickUpPointsViewFactory));
            _playerProviderSetter = playerProviderSetter ??
                                    throw new ArgumentNullException(nameof(playerProviderSetter));
            _tavernProviderSetter = tavernProviderSetter ??
                                    throw new ArgumentNullException(nameof(tavernProviderSetter));
            _upgradeProviderSetter = upgradeProviderSetter ??
                                     throw new ArgumentNullException(nameof(upgradeProviderSetter));
            _tavernMoodViewFactory = tavernMoodViewFactory ??
                                     throw new ArgumentNullException(nameof(tavernMoodViewFactory));
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
            _textUIFactory = textUIFactory ?? throw new ArgumentNullException(nameof(textUIFactory));
            PlayerDataService = playerDataService;
            PlayerUpgradeDataService = playerUpgradeDataService ??
                                       throw new ArgumentNullException(nameof(playerUpgradeDataService));
            TavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _gameplayFormServiceFactory = gameplayFormServiceFactory ??
                                          throw new ArgumentNullException(nameof(gameplayFormServiceFactory));
            _visitorPointsRepositoryFactory = visitorPointsRepositoryFactory ??
                                              throw new ArgumentNullException(nameof(visitorPointsRepositoryFactory));
            _playerViewFactory = playerViewFactory ??
                                 throw new ArgumentNullException(nameof(playerViewFactory));
        }

        public IDataService<Player> PlayerDataService { get; private set; }
        public IDataService<PlayerUpgrade> PlayerUpgradeDataService { get; private set; }
        public IDataService<Tavern> TavernDataService { get; private set; }

        public void Load()
        {
            _player = CreatePlayer();
            _playerUpgrade = CreatePlayerUpgrade();
            _tavern = CreateTavern();

            //UpgradeProviders
            _upgradeProviderSetter.SetCharisma(_playerUpgrade.Charisma);
            _upgradeProviderSetter.SetInventory(_playerUpgrade.Inventory);
            _upgradeProviderSetter.SetMovement(_playerUpgrade.Movement);

            _tavernProviderSetter.SetTavernMood(_tavern.TavernMood);
            _tavernProviderSetter.SetGameplay(_tavern.VisitorQuantity);

            _playerProviderSetter.SetInventory(_player.Inventory);
            _playerProviderSetter.SetMovement(_player.Movement);
            _playerProviderSetter.SetWallet(_player.Wallet);

            //Items
            var itemConfigContainer = Resources
                .Load<ItemConfigContainer>(Constant.PrefabPaths.ItemConfigContainer);

            IItem[] items =
            {
                new Beer(itemConfigContainer.Beer),
                new Bread(itemConfigContainer.Bread),
                new Meat(itemConfigContainer.Meat),
                new Soup(itemConfigContainer.Soup),
                new Wine(itemConfigContainer.Wine)
            };

            _itemProvider.AddCollection(items);

            //EatAndSeatGamePoints
            _visitorPointsRepositoryFactory.Create();

            //HudText
            _textUIFactory.Create(_hud.TextUIContainer.PlayerWalletText, _player.Wallet.Coins);
            _hud.TextUIContainer.PlayerWalletText.SetText(_player.Wallet.Coins.StringValue);

            //PlayerView
            _playerViewFactory.Create(_player, _playerCameraView);

            //TavernMood
            _imageUIFactory.Create(_hud.TavernMoodImageUI);
            _tavernMoodViewFactory.Create(_hud.TavernMoodView, _tavern.TavernMood, _hud.TavernMoodImageUI);

            //BackgroundMusicView
            _backgroundMusicViewFactory.Create(_hud.BackgroundMusicView);

            //TavernPickUpPoints
            var foodPickUpPointContainer = new FoodPickUpPointContainer(
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint());

            _foodPickUpPointsViewFactory.Create(foodPickUpPointContainer, itemConfigContainer, _rootGamePoints);

            //FormService
            var gameplayFormService = _gameplayFormServiceFactory
                .Create(_playerUpgrade, _player, _hud, Save);

            gameplayFormService.Show<HudFormView>();

            ShowLoadForm(gameplayFormService);
        }

        public void Save()
        {
            PlayerDataService.Save(_player);
            PlayerUpgradeDataService.Save(_playerUpgrade);
            TavernDataService.Save(_tavern);
        }

        protected abstract Player CreatePlayer();
        protected abstract PlayerUpgrade CreatePlayerUpgrade();
        protected abstract Tavern CreateTavern();

        private async void ShowLoadForm(IFormService gameplayFormService)
        {
            if (_setting.Tutorial.HasCompleted == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Constant.App.CurtainWaiting));

                gameplayFormService.Show<TutorialFormView>();

                return;
            }

            if (CanLoad())
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Constant.App.CurtainWaiting));

                gameplayFormService.Show<LoadFormView>();
            }
        }

        private bool CanLoad()
        {
            return PlayerDataService.CanLoad && TavernDataService.CanLoad && PlayerUpgradeDataService.CanLoad;
        }
    }
}