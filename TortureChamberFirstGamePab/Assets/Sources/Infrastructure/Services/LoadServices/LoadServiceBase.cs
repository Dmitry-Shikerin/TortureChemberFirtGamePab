using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.UI.AudioSources;
using Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.Presentation.Containers.GamePoints;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Voids;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public abstract class LoadServiceBase : ILoadService
    {
        protected readonly IDataService<Player> PlayerDataService;
        protected readonly IDataService<PlayerUpgrade> PlayerUpgradeDataService;
        protected readonly IDataService<Tavern> TavernDataService;

        private readonly ImageUIFactory _imageUIFactory;
        private readonly IPrefabFactory _prefabFactory;
        private readonly GameplayFormServiceFactory _gameplayFormServiceFactory;
        private readonly VisitorPointsRepositoryFactory _visitorPointsRepositoryFactory;
        private readonly PlayerViewFactory _playerViewFactory;
        private readonly HUD _hud;
        private readonly RootGamePoints _rootGamePoints;
        private readonly PlayerCameraView _playerCameraView;
        private readonly BackgroundMusicViewFactory _backgroundMusicViewFactory;
        private readonly Setting _setting;
        private readonly FoodPickUpPointsViewFactory _foodPickUpPointsViewFactory;
        private readonly AudioSourceUIFactory _audioSourceUIFactory;
        private readonly IPauseService _pauseService;
        private readonly PauseMenuService _pauseMenuService;
        private readonly CollectionRepository _collectionRepository;
        private readonly EatPointViewFactory _eatPointViewFactory;
        private readonly SeatPointViewFactory _seatPointViewFactory;
        private readonly IPlayerProviderSetter _playerProviderSetter;
        private readonly ITavernProviderSetter _tavernProviderSetter;
        private readonly IUpgradeProviderSetter _upgradeProviderSetter;
        private readonly TavernFoodPickUpPointViewFactory _tavernFoodPickUpPointViewFactory;
        private readonly TavernMoodViewFactory _tavernMoodViewFactory;
        private readonly PlayerUpgradeViewFactory _playerUpgradeViewFactory;
        private readonly ItemProvider<IItem> _itemProvider;
        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PlayerInventoryViewFactory _playerInventoryViewFactory;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly TextUIFactory _textUIFactory;
        private readonly ButtonUIFactory _buttonUIFactory;

        private Player _player;
        private PlayerUpgrade _playerUpgrade;
        private Tavern _tavern;

        protected LoadServiceBase
        (
            BackgroundMusicViewFactory backgroundMusicViewFactory,
            Setting setting,
            FoodPickUpPointsViewFactory foodPickUpPointsViewFactory,
            AudioSourceUIFactory audioSourceUIFactory,
            PlayerCameraView playerCameraView,
            IPauseService pauseService,
            PauseMenuService pauseMenuService,
            CollectionRepository collectionRepository,
            EatPointViewFactory eatPointViewFactory,
            SeatPointViewFactory seatPointViewFactory,
            RootGamePoints rootGamePoints,
            IPlayerProviderSetter playerProviderSetter,
            ITavernProviderSetter tavernProviderSetter,
            IUpgradeProviderSetter upgradeProviderSetter,
            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory,
            TavernMoodViewFactory tavernMoodViewFactory,
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            HUD hud,
            ItemProvider<IItem> itemProvider,
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            ImageUIFactory imageUIFactory,
            IPrefabFactory prefabFactory,
            GameplayFormServiceFactory gameplayFormServiceFactory,
            VisitorPointsRepositoryFactory visitorPointsRepositoryFactory,
            PlayerViewFactory playerViewFactory
        )
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _rootGamePoints = rootGamePoints ? rootGamePoints : throw new ArgumentNullException(nameof(rootGamePoints));
            _playerCameraView = playerCameraView
                ? playerCameraView
                : throw new ArgumentNullException(nameof(playerCameraView));
            _backgroundMusicViewFactory = backgroundMusicViewFactory ?? throw new ArgumentNullException(nameof(backgroundMusicViewFactory));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _foodPickUpPointsViewFactory = foodPickUpPointsViewFactory ??
                                           throw new ArgumentNullException(nameof(foodPickUpPointsViewFactory));
            _audioSourceUIFactory =
                audioSourceUIFactory ?? throw new ArgumentNullException(nameof(audioSourceUIFactory));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _eatPointViewFactory = eatPointViewFactory ??
                                   throw new ArgumentNullException(nameof(eatPointViewFactory));
            _seatPointViewFactory = seatPointViewFactory ??
                                    throw new ArgumentNullException(nameof(seatPointViewFactory));
            _playerProviderSetter = playerProviderSetter ??
                                    throw new ArgumentNullException(nameof(playerProviderSetter));
            _tavernProviderSetter = tavernProviderSetter ??
                                    throw new ArgumentNullException(nameof(tavernProviderSetter));
            _upgradeProviderSetter = upgradeProviderSetter ??
                                     throw new ArgumentNullException(nameof(upgradeProviderSetter));
            _tavernFoodPickUpPointViewFactory =
                tavernFoodPickUpPointViewFactory ??
                throw new ArgumentNullException(nameof(tavernFoodPickUpPointViewFactory));
            _tavernMoodViewFactory = tavernMoodViewFactory ??
                                     throw new ArgumentNullException(nameof(tavernMoodViewFactory));
            _playerUpgradeViewFactory = playerUpgradeViewFactory ??
                                        throw new ArgumentNullException(nameof(playerUpgradeViewFactory));
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
            _playerMovementViewFactory = playerMovementViewFactory ??
                                         throw new ArgumentNullException(nameof(playerMovementViewFactory));
            _playerCameraViewFactory = playerCameraViewFactory ??
                                       throw new ArgumentNullException(nameof(playerCameraViewFactory));
            _playerInventoryViewFactory = playerInventoryViewFactory ??
                                          throw new ArgumentNullException(nameof(playerInventoryViewFactory));
            _playerWalletViewFactory = playerWalletViewFactory ??
                                       throw new ArgumentNullException(nameof(playerWalletViewFactory));
            _textUIFactory = textUIFactory ?? throw new ArgumentNullException(nameof(textUIFactory));
            _buttonUIFactory = buttonUIFactory ??
                               throw new ArgumentNullException(nameof(buttonUIFactory));
            PlayerDataService = playerDataService;
            PlayerUpgradeDataService = playerUpgradeDataService ??
                                       throw new ArgumentNullException(nameof(playerUpgradeDataService));
            TavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _gameplayFormServiceFactory = gameplayFormServiceFactory ??
                                          throw new ArgumentNullException(nameof(gameplayFormServiceFactory));
            _visitorPointsRepositoryFactory = visitorPointsRepositoryFactory ??
                                              throw new ArgumentNullException(nameof(visitorPointsRepositoryFactory));
            _playerViewFactory = playerViewFactory ??
                                 throw new ArgumentNullException(nameof(playerViewFactory));
        }

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
            ItemConfigContainer itemConfigContainer = Resources
                .Load<ItemConfigContainer>(Constant.PrefabPaths.ItemConfigContainer);

            IItem[] items = new IItem[]
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

            //TODO изменить якоря у формочек юая
            //TODO спросить у славы как мы делали?
            //TODO добавить плагин для ресайза игры
            //BackgroundMusicView
            Debug.Log("_backgroundMusicViewFactory Create");
            _backgroundMusicViewFactory.Create(_hud.BackgroundMusicView);
            
            //TavernPickUpPoints
            FoodPickUpPointContainer foodPickUpPointContainer = new FoodPickUpPointContainer
            (
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint(),
                new FoodPickUpPoint()
            );

            _foodPickUpPointsViewFactory.Create(foodPickUpPointContainer, itemConfigContainer, _rootGamePoints);

            //FormService
            IFormService gameplayFormService = _gameplayFormServiceFactory
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

        private bool CanLoad() => 
            PlayerDataService.CanLoad && TavernDataService.CanLoad && PlayerUpgradeDataService.CanLoad;
    }
}