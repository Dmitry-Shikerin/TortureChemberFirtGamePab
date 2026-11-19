using System;
using Controllers.Forms.Gameplays;
using InfrastructureInterfaces.Services.Forms;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Infrastructure.Factories.Controllers.Forms.Gameplays
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