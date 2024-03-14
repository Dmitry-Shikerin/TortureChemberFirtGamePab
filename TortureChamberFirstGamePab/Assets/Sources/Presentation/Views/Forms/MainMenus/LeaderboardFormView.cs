using Sources.Controllers.Forms.MainMenus;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Presentation.Views.Forms.MainMenus
{
    public class LeaderboardFormView : FormBase<LeaderboardFormPresenter>, ILeaderboardFormView
    {
        public void ShowMainMenu()
        {
            Presenter.ShowMainMenu();
        }
    }
}