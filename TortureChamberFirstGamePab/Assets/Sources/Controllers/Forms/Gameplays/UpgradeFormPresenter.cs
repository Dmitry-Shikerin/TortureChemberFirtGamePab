using System;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Controllers.Forms.Gameplays
{
    public class UpgradeFormPresenter : PresenterBase
    {
        private readonly IUpgradeFormView _leaderboardFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly IDataService<Domain.DataAccess.Containers.Players.Player> _playerDataService;
        private readonly IDataService<PlayerUpgrade> _playerUpgradeDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IPlayerProvider _playerProvider;
        private readonly IUpgradeProvider _upgradeProvider;
        private readonly ITavernProvider _tavernProvider;

        public UpgradeFormPresenter
        (
            IUpgradeFormView leaderboardFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _leaderboardFormView = leaderboardFormView ??
                                   throw new ArgumentNullException(nameof(leaderboardFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable() =>
            _pauseService.Pause();

        public override void Disable()
        {
            _pauseService.Continue();
        }

        public void ShowHudForm() =>
            _formService.Show<HudFormView>();
    }
}