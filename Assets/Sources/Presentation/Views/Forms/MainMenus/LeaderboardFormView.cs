using Controllers.Forms.MainMenus;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.MainMenus;

namespace Presentation.Views.Forms.MainMenus
{
    public class LeaderboardFormView : FormBase<LeaderboardFormPresenter>, ILeaderboardFormView
    {
        public void ShowMainMenu() =>
            Presenter.ShowMainMenu();
    }
}