using Sources.Controllers.Forms.MainMenus;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Presentation.Views.Forms.MainMenus
{
    public class MainMenuFormView : FormBase<MainMenuFormPresenter>, IMainMenuFormView
    {
        public void ShowLeaderboard() => 
            Presenter.ShowLeaderBoard();

        public void ShowSetting() => 
            Presenter.ShowSetting();
    }
}