using System;
using Scripts.Controllers.Forms.MainMenus;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;

namespace Scripts.Infrastructure.Factories.Controllers.Forms.MainMenus
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