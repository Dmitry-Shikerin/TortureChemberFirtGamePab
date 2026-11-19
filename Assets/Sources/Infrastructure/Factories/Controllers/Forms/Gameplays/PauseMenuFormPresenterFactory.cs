using System;
using Controllers.Forms.Gameplays;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.PauseServices;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class PauseMenuFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public PauseMenuFormPresenterFactory(
            IFormService formService,
            IPauseService pauseService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public PauseMenuFormPresenter Create(IPauseMenuFormView pauseMenuFormView)
        {
            if (pauseMenuFormView == null)
                throw new ArgumentNullException(nameof(pauseMenuFormView));

            return new PauseMenuFormPresenter(_formService, _pauseService);
        }
    }
}