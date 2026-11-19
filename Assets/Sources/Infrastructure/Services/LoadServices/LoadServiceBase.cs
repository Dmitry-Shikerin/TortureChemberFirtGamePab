using System;
using Cysharp.Threading.Tasks;
using Domain.Constants;
using Domain.DataAccess.Containers.Players;
using Domain.DataAccess.Containers.Settings;
using Domain.DataAccess.Containers.Taverns;
using Domain.Items;
using Domain.Items.ItemConfigs;
using Domain.Taverns;
using DomainInterfaces.Items;
using Infrastructure.Factories.Repositoryes;
using Infrastructure.Factories.Services.Forms;
using Infrastructure.Factories.Views.Players;
using Infrastructure.Factories.Views.Taverns;
using Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Infrastructure.Factories.Views.UI;
using Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.LoadServices;
using InfrastructureInterfaces.Services.LoadServices.Components;
using InfrastructureInterfaces.Services.Providers.Players;
using InfrastructureInterfaces.Services.Providers.Taverns;
using InfrastructureInterfaces.Services.Providers.Upgrades;
using Presentation.Containers.GamePoints;
using Presentation.Containers.HUDs;
using Presentation.Views.Forms.Gameplays;
using Presentation.Views.Player;
using UnityEngine;
using Utils.Repositories.ItemRepository;

namespace Infrastructure.Services.LoadServices
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

        protected IDataService<Player> PlayerDataService { get; }
        protected IDataService<PlayerUpgrade> PlayerUpgradeDataService { get; }
        protected IDataService<Tavern> TavernDataService { get; }

        public void Load()
        {
            _player = CreatePlayer();
            _playerUpgrade = CreatePlayerUpgrade();
            _tavern = CreateTavern();

            _upgradeProviderSetter.SetCharisma(_playerUpgrade.Charisma);
            _upgradeProviderSetter.SetInventory(_playerUpgrade.Inventory);
            _upgradeProviderSetter.SetMovement(_playerUpgrade.Movement);

            _tavernProviderSetter.SetTavernMood(_tavern.TavernMood);
            _tavernProviderSetter.SetGameplay(_tavern.VisitorQuantity);

            _playerProviderSetter.SetInventory(_player.Inventory);
            _playerProviderSetter.SetMovement(_player.Movement);
            _playerProviderSetter.SetWallet(_player.Wallet);

            var itemConfigContainer = Resources
                .Load<ItemConfigContainer>(PrefabPath.ItemConfigContainer);

            IItem[] items =
            {
                new Beer(itemConfigContainer.Beer),
                new Bread(itemConfigContainer.Bread),
                new Meat(itemConfigContainer.Meat),
                new Soup(itemConfigContainer.Soup),
                new Wine(itemConfigContainer.Wine),
            };

            _itemProvider.AddCollection(items);

            _visitorPointsRepositoryFactory.Create();

            _textUIFactory.Create(_hud.TextUIContainer.PlayerWalletText, _player.Wallet.Coins);
            _hud.TextUIContainer.PlayerWalletText.SetText(_player.Wallet.Coins.StringValue);

            _playerViewFactory.Create(_player, _playerCameraView);

            _imageUIFactory.Create(_hud.TavernMoodImageUI);
            _tavernMoodViewFactory.Create(_hud.TavernMoodView, _tavern.TavernMood, _hud.TavernMoodImageUI);

            _backgroundMusicViewFactory.Create(_hud.BackgroundMusicView);

            var foodPickUpPointContainer = new FoodPickUpPointContainer(
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint());

            _foodPickUpPointsViewFactory.Create(foodPickUpPointContainer, itemConfigContainer, _rootGamePoints);

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
            PlayerPrefs.Save();
        }

        protected abstract Player CreatePlayer();
        protected abstract PlayerUpgrade CreatePlayerUpgrade();
        protected abstract Tavern CreateTavern();

        private async void ShowLoadForm(IFormService gameplayFormService)
        {
            if (_setting.Tutorial.HasCompleted == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstant.CurtainWaiting));

                gameplayFormService.Show<TutorialFormView>();

                return;
            }

            if (CanLoad())
            {
                await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstant.CurtainWaiting));

                gameplayFormService.Show<LoadFormView>();
            }
        }

        public bool CanLoad() =>
            PlayerDataService.CanLoad && TavernDataService.CanLoad && PlayerUpgradeDataService.CanLoad;
    }
}