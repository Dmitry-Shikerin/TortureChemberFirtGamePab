using System;
using InfrastructureInterfaces.Services.Forms;
using Presentation.Views.Forms;
using Presentation.Views.Forms.MainMenus;

namespace Controllers.Forms.MainMenus
{
    public class MainMenuFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;

        public MainMenuFormPresenter(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public void ShowLeaderBoard() =>
            _formService.Show<LeaderboardFormView>();

        public void ShowSetting() =>
            _formService.Show<SettingFormView>();
    }
}