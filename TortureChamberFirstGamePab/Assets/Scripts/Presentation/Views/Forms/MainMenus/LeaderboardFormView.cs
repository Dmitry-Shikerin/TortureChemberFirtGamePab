using Scripts.Controllers.Forms.MainMenus;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;

namespace Scripts.Presentation.Views.Forms.MainMenus
{
    public class LeaderboardFormView : FormBase<LeaderboardFormPresenter>, ILeaderboardFormView
    {
        public void ShowMainMenu() =>
            Presenter.ShowMainMenu();
    }
}