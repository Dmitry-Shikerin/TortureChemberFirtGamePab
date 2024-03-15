using System;
using Scripts.Controllers.Forms.Gameplays;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;

namespace Scripts.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class HudFormPresenterFactory
    {
        private readonly IFormService _formService;

        public HudFormPresenterFactory(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public HudFormPresenter Create(IHudFormView hudFormView)
        {
            if (hudFormView == null)
                throw new ArgumentNullException(nameof(hudFormView));

            return new HudFormPresenter(_formService);
        }
    }
}