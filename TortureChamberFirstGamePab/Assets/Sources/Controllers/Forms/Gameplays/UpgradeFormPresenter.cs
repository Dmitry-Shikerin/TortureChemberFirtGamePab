using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Controllers.Forms.Gameplays
{
    public class UpgradeFormPresenter : PresenterBase
    {
        private readonly IUpgradeFormView _leaderboardFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

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

        public override void Disable() => 
            _pauseService.Continue();
    }
}