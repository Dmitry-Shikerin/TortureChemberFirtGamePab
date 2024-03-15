using System;
using Scripts.Controllers.Forms.Gameplays;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;

namespace Scripts.Infrastructure.Factories.Controllers.Forms.Gameplays
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