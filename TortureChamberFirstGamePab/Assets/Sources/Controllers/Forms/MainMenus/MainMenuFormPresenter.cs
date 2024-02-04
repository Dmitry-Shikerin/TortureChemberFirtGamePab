using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Controllers.Forms.MainMenus
{
    public class MainMenuFormPresenter : PresenterBase
    {
        private readonly IMainMenuFormView _mainMenuFormView;
        private readonly IFormService _formService;

        public MainMenuFormPresenter(IMainMenuFormView mainMenuFormView, IFormService formService)
        {
            _mainMenuFormView = mainMenuFormView ?? throw new ArgumentNullException(nameof(mainMenuFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public void ShowLeaderBoard() => 
            _formService.Show<LeaderboardFormView>();
    }
}