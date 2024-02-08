using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players.PlayerCameras;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Players;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;
using Object = UnityEngine.Object;

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
            _rootGamePoints = rootGamePoints ? rootGamePoints : 
                throw new ArgumentNullException(nameof(rootGamePoints));
            _playerCameraView = playerCameraView 
                ? playerCameraView 
                : throw new ArgumentNullException(nameof(playerCameraView));
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
            _upgradeProviderSetter.SetCharisma(_playerUpgrade.CharismaUpgrader);
            _upgradeProviderSetter.SetInventory(_playerUpgrade.InventoryUpgrader);
            _upgradeProviderSetter.SetMovement(_playerUpgrade.MovementUpgrader);
            
            _tavernProviderSetter.SetTavernMood(_tavern.TavernMood);
            _tavernProviderSetter.SetGameplay(_tavern.VisitorQuantity);
            
            //Items
            ItemConfigContainer container = Resources
                .Load<ItemConfigContainer>(Constant.PrefabPaths.ItemConfigContainer);

            IItem[] items = new IItem[]
            {
                new Beer(container.Beer),
                new Bread(container.Bread),
                new Meat(container.Meat),
                new Soup(container.Soup),
                new Wine(container.Wine)
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
            
            //TavernPickUpPoints
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.BeerPickUpPointView, container.Beer);
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.BreadPickUpPointView, container.Bread);
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.MeatPickUpPointView, container.Meat);
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.SoupPickUpPointView, container.Soup);
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.WinePickUpPointView, container.Wine);
            
            //TODO запускать приходится здесь
            //FormService
            _gameplayFormServiceFactory.
                Create(_playerUpgrade, _player, _hud)
                .Show<HudFormView>();
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
    }
}