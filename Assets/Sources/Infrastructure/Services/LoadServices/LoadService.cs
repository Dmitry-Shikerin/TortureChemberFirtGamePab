using Domain.DataAccess.Containers.Players;
using Domain.DataAccess.Containers.Settings;
using Domain.DataAccess.Containers.Taverns;
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
using Utils.Repositories.ItemRepository;

namespace Infrastructure.Services.LoadServices
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