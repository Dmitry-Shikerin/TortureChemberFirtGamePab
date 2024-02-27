using System;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Controllers.Forms.MainMenus
{
    public class LeaderboardFormPresenter : PresenterBase
    {
        private readonly ILeaderboardFormView _leaderboardFormView;
        private readonly IFormService _formService;
        private readonly ILeaderboardInitializeService _leaderboardInitializeService;

        public LeaderboardFormPresenter
        (
            ILeaderboardFormView leaderboardFormView,
            IFormService formService,
            ILeaderboardInitializeService leaderboardInitializeService
        )
        {
            _leaderboardFormView = leaderboardFormView ??
                                   throw new ArgumentNullException(nameof(leaderboardFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _leaderboardInitializeService = leaderboardInitializeService ??
                                            throw new ArgumentNullException(nameof(leaderboardInitializeService));
        }

        public void ShowMainMenu() =>
            _formService.Show<MainMenuFormView>();

        public override void Enable()
        {
            _leaderboardInitializeService.Fill();
        }

        public override void Disable()
        {
            base.Disable();
        }
    }
}