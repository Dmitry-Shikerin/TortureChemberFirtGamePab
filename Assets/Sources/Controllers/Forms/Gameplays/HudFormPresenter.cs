using System;
using InfrastructureInterfaces.Services.Forms;
using Presentation.Views.Forms.Gameplays;

namespace Controllers.Forms.Gameplays
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