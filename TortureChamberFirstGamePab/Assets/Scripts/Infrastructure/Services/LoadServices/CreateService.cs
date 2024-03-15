using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.DataAccess.Containers.Taverns;
using Scripts.Domain.GamePlays;
using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;
using Scripts.Domain.Taverns;
using Scripts.Domain.Upgrades;
using Scripts.Domain.Upgrades.Configs.Containers;
using Scripts.DomainInterfaces.Items;
using Scripts.Infrastructure.Factories.Repositoryes;
using Scripts.Infrastructure.Factories.Services.Forms;
using Scripts.Infrastructure.Factories.Views.Players;
using Scripts.Infrastructure.Factories.Views.Taverns;
using Scripts.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Scripts.Infrastructure.Factories.Views.UI;
using Scripts.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.Providers.Players;
using Scripts.InfrastructureInterfaces.Services.Providers.Taverns;
using Scripts.InfrastructureInterfaces.Services.Providers.Upgrades;
using Scripts.Presentation.Containers.GamePoints;
using Scripts.Presentation.Containers.HUDs;
using Scripts.Presentation.Views.Player;
using Scripts.Utils.Repositories.ItemRepository;
using UnityEngine;

namespace Scripts.Infrastructure.Services.LoadServices
{
    public class CreateService : LoadServiceBase
    {
        public CreateService(
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
            : base(
                backgroundMusicViewFactory,
                setting,
                foodPickUpPointsViewFactory,
                playerCameraView,
                rootGamePoints,
                playerProviderSetter,
                tavernProviderSetter,
                upgradeProviderSetter,
                tavernMoodViewFactory,
                hud,
                itemProvider,
                textUIFactory,
                playerDataService,
                playerUpgradeDataService,
                tavernDataService,
                imageUIFactory,
                gameplayFormServiceFactory,
                visitorPointsRepositoryFactory,
                playerViewFactory)
        {
        }

        protected override Player CreatePlayer()
        {
            PlayerMovement playerMovement = new PlayerMovement();
            PlayerInventory playerInventory = new PlayerInventory();
            PlayerWallet playerWallet = new PlayerWallet();

            Player player = new Player(playerMovement, playerInventory, playerWallet);

            return player;
        }

        protected override PlayerUpgrade CreatePlayerUpgrade()
        {
            UpgradeConfigContainer container = Resources
                .Load<UpgradeConfigContainer>(PrefabPath.UpgradeConfigContainer);

            Upgrader charismaUpgrader = new Upgrader(container.Charisma);
            Upgrader inventoryUpgrader = new Upgrader(container.Inventory);
            Upgrader movementUpgrader = new Upgrader(container.Movement);

            PlayerUpgrade playerUpgrade = new PlayerUpgrade(
                charismaUpgrader, inventoryUpgrader, movementUpgrader);

            return playerUpgrade;
        }

        protected override Tavern CreateTavern()
        {
            TavernMood tavernMood = new TavernMood();
            VisitorQuantity visitorQuantity = new VisitorQuantity();

            Tavern tavern = new Tavern(tavernMood, visitorQuantity);

            return tavern;
        }
    }
}