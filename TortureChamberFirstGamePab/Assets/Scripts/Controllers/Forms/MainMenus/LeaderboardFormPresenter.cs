using System;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.Presentation.Views.Forms.MainMenus;

namespace Scripts.Controllers.Forms.MainMenus
{
    public class LeaderboardFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;

        public LeaderboardFormPresenter(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public void ShowMainMenu() =>
            _formService.Show<MainMenuFormView>();
    }
}