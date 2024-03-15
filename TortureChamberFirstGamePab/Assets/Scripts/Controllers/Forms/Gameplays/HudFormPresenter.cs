using System;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.Presentation.Views.Forms.Gameplays;

namespace Scripts.Controllers.Forms.Gameplays
{
    public class HudFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;

        public HudFormPresenter(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public void ShowPauseMenu() =>
            _formService.Show<PauseMenuFormView>();
    }
}