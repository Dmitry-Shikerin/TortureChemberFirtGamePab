using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.DataAccess.Containers.Taverns;
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

namespace Scripts.Infrastructure.Services.LoadServices
{
    public class LoadService : LoadServiceBase
    {
        public LoadService(
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
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            TextUIFactory textUIFactory,
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

        protected override Player CreatePlayer() =>
            PlayerDataService.Load();

        protected override PlayerUpgrade CreatePlayerUpgrade() =>
            PlayerUpgradeDataService.Load();

        protected override Tavern CreateTavern() =>
            TavernDataService.Load();
    }
}