using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.GamePlays;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Domain.Taverns;
using Sources.Domain.Upgrades;
using Sources.Domain.Upgrades.Configs.Containers;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.Presentation.Containers.GamePoints;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Voids;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
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
            var playerMovement = new PlayerMovement();
            var playerInventory = new PlayerInventory();
            var playerWallet = new PlayerWallet();

            var player = new Player(playerMovement, playerInventory, playerWallet);

            return player;
        }

        protected override PlayerUpgrade CreatePlayerUpgrade()
        {
            var container = Resources
                .Load<UpgradeConfigContainer>(Constant.PrefabPaths.UpgradeConfigContainer);

            var charismaUpgrader = new Upgrader(container.Charisma);
            var inventoryUpgrader = new Upgrader(container.Inventory);
            var movementUpgrader = new Upgrader(container.Movement);

            var playerUpgrade = new PlayerUpgrade(charismaUpgrader,
                inventoryUpgrader,
                movementUpgrader);

            return playerUpgrade;
        }

        protected override Tavern CreateTavern()
        {
            var tavernMood = new TavernMood();
            var visitorQuantity = new VisitorQuantity();

            var tavern = new Tavern(tavernMood, visitorQuantity);

            return tavern;
        }
    }
}