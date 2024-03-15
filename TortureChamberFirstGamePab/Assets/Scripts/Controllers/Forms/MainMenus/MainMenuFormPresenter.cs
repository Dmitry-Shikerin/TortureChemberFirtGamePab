using System;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.Presentation.Views.Forms;
using Scripts.Presentation.Views.Forms.MainMenus;

namespace Scripts.Controllers.Forms.MainMenus
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