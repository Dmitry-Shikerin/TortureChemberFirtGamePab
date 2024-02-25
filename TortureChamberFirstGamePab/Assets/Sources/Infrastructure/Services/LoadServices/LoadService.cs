using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.UI.AudioSources;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.Presentation.Containers.GamePoints;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Voids;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class LoadService : LoadServiceBase
    {
        public LoadService
        (
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
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            ImageUIFactory imageUIFactory,
            IPrefabFactory prefabFactory,
            GameplayFormServiceFactory gameplayFormServiceFactory,
            VisitorPointsRepositoryFactory visitorPointsRepositoryFactory,
            PlayerViewFactory playerViewFactory
        ) :
            base
            (
                setting,
                foodPickUpPointsViewFactory,
                audioSourceUIFactory,
                playerCameraView,
                pauseService,
                pauseMenuService,
                collectionRepository,
                eatPointViewFactory,
                seatPointViewFactory,
                rootGamePoints,
                playerProviderSetter,
                tavernProviderSetter,
                upgradeProviderSetter,
                tavernFoodPickUpPointViewFactory,
                tavernMoodViewFactory,
                playerUpgradeViewFactory,
                hud,
                itemProvider,
                playerMovementViewFactory,
                playerCameraViewFactory,
                playerInventoryViewFactory,
                playerWalletViewFactory,
                textUIFactory,
                buttonUIFactory,
                playerDataService,
                playerUpgradeDataService,
                tavernDataService,
                imageUIFactory,
                prefabFactory,
                gameplayFormServiceFactory,
                visitorPointsRepositoryFactory,
                playerViewFactory
            )
        {
        }

        protected override Player CreatePlayer()
        {
            Debug.Log("Load scene");
            return PlayerDataService.Load();
        }

        protected override PlayerUpgrade CreatePlayerUpgrade() => 
            PlayerUpgradeDataService.Load();

        protected override Tavern CreateTavern() => 
            TavernDataService.Load();
    }
}