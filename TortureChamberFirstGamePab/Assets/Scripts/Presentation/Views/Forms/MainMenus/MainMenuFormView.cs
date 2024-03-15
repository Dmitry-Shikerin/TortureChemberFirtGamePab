using Scripts.Controllers.Forms.MainMenus;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;

namespace Scripts.Presentation.Views.Forms.MainMenus
{
    public class MainMenuFormView : FormBase<MainMenuFormPresenter>, IMainMenuFormView
    {
        public void ShowLeaderboard() =>
            Presenter.ShowLeaderBoard();

        public void ShowSetting() =>
            Presenter.ShowSetting();
    }
}