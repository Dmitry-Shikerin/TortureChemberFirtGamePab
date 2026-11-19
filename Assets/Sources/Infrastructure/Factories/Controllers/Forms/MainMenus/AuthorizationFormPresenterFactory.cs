using System;
using Controllers.Forms.MainMenus;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.SDCServices;
using PresentationInterfaces.Views.Forms.MainMenus;

namespace Infrastructure.Factories.Controllers.Forms.MainMenus
{
    public class AuthorizationFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPlayerAccountAuthorizeService _playerAccountAuthorizeService;

        public AuthorizationFormPresenterFactory(
            IFormService formService,
            IPlayerAccountAuthorizeService playerAccountAuthorizeService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _playerAccountAuthorizeService = playerAccountAuthorizeService ??
                                             throw new ArgumentNullException(nameof(playerAccountAuthorizeService));
        }

        public AuthorizationFormPresenter Create(IAuthorizationFormView authorizationFormView)
        {
            return new AuthorizationFormPresenter(
                authorizationFormView,
                _formService,
                _playerAccountAuthorizeService);
        }
    }
}