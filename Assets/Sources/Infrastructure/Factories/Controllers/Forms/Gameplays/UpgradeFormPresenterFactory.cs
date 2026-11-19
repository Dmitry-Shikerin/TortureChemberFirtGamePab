using System;
using Controllers.Forms.Gameplays;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.PauseServices;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class UpgradeFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public UpgradeFormPresenterFactory(IFormService formService, IPauseService pauseService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public UpgradeFormPresenter Create(IUpgradeFormView upgradeFormView)
        {
            if (upgradeFormView == null)
                throw new ArgumentNullException(nameof(upgradeFormView));

            return new UpgradeFormPresenter(_formService, _pauseService);
        }
    }
}