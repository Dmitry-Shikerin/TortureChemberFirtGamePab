using Controllers.Forms.MainMenus;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.MainMenus;

namespace Presentation.Views.Forms.MainMenus
{
    public class MainMenuFormView : FormBase<MainMenuFormPresenter>, IMainMenuFormView
    {
        public void ShowLeaderboard() =>
            Presenter.ShowLeaderBoard();

        public void ShowSetting() =>
            Presenter.ShowSetting();
    }
}