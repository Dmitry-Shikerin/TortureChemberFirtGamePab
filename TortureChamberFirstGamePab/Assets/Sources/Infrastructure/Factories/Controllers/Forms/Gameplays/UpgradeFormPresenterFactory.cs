using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
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
            
            return new UpgradeFormPresenter(upgradeFormView, _formService, _pauseService);
        }
    }
}