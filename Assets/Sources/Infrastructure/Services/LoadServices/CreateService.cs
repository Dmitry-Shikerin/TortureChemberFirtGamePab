using Domain.Constants;
using Domain.DataAccess.Containers.Players;
using Domain.DataAccess.Containers.Settings;
using Domain.DataAccess.Containers.Taverns;
using Domain.GamePlays;
using Domain.Players;
using Domain.Players.PlayerMovements;
using Domain.Taverns;
using Domain.Upgrades;
using Domain.Upgrades.Configs.Containers;
using DomainInterfaces.Items;
using Infrastructure.Factories.Repositoryes;
using Infrastructure.Factories.Services.Forms;
using Infrastructure.Factories.Views.Players;
using Infrastructure.Factories.Views.Taverns;
using Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Infrastructure.Factories.Views.UI;
using Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using InfrastructureInterfaces.Services.LoadServices.Components;
using InfrastructureInterfaces.Services.Providers.Players;
using InfrastructureInterfaces.Services.Providers.Taverns;
using InfrastructureInterfaces.Services.Providers.Upgrades;
using Presentation.Containers.GamePoints;
using Presentation.Containers.HUDs;
using Presentation.Views.Player;
using UnityEngine;
using Utils.Repositories.ItemRepository;

namespace Infrastructure.Services.LoadServices
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