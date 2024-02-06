using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class UpgradeFormPresenterFactory
    {
        private readonly IFormService _formService;

        public UpgradeFormPresenterFactory(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public UpgradeFormPresenter Create(IUpgradeFormView upgradeFormView)
        {
            return new UpgradeFormPresenter(upgradeFormView, _formService);
        }
    }
}