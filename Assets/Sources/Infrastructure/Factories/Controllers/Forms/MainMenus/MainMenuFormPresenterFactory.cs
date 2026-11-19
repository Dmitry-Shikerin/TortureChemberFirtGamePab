using System;
using Controllers.Forms.MainMenus;
using InfrastructureInterfaces.Services.Forms;
using PresentationInterfaces.Views.Forms.MainMenus;

namespace Infrastructure.Factories.Controllers.Forms.MainMenus
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