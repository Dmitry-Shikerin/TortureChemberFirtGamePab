using System;
using Controllers.Forms.Gameplays;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.PauseServices;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class LoadFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public LoadFormPresenterFactory(
            IFormService formService,
            IPauseService pauseService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public LoadFormPresenter Create(ILoadFormView pauseMenuFormView)
        {
            if (pauseMenuFormView == null)
                throw new ArgumentNullException(nameof(pauseMenuFormView));

            return new LoadFormPresenter(_formService, _pauseService);
        }
    }
}