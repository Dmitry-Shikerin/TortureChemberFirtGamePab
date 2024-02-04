using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Infrastructure.Factories.Controllers.Forms.MainMenus
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
            return new MainMenuFormPresenter(mainMenuFormView, _formService);
        }
    }
}