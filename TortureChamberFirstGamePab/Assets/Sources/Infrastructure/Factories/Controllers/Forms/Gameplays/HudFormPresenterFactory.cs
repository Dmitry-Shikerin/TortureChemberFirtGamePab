using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
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
            return new HudFormPresenter(hudFormView, _formService);
        }
    }
}