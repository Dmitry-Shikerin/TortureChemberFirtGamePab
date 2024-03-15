using System;
using Scripts.Controllers.Forms.MainMenus;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;

namespace Scripts.Infrastructure.Factories.Controllers.Forms.MainMenus
{
    public class MainMenuFormPresenterFactory
    {
        private readonly IFormService _formService;

        public MainMenuFormPresenterFactory(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public MainMenuFormPresenter Create(IMainMenuFormView mainMenuFormView)
        {
            if (mainMenuFormView == null)
                throw new ArgumentNullException(nameof(mainMenuFormView));

            return new MainMenuFormPresenter(_formService);
        }
    }
}