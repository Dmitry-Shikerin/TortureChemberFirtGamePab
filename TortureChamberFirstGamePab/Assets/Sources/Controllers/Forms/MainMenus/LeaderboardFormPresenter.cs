using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Controllers.Forms.MainMenus
{
    public class LeaderboardFormPresenter : PresenterBase
    {
        private readonly ILeaderboardFormView _leaderboardFormView;
        private readonly IFormService _formService;

        public LeaderboardFormPresenter(ILeaderboardFormView leaderboardFormView, IFormService formService)
        {
            _leaderboardFormView = leaderboardFormView ?? 
                                   throw new ArgumentNullException(nameof(leaderboardFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public void ShowMainMenu() => 
            _formService.Show<MainMenuFormView>();
    }
}