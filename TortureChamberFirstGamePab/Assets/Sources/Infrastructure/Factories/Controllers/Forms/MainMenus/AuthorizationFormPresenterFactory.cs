using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Infrastructure.Factories.Controllers.Forms.MainMenus
{
    public class AuthorizationFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPlayerAccountAuthorizeService _playerAccountAuthorizeService;

        public AuthorizationFormPresenterFactory
        (
            IFormService formService,
            IPlayerAccountAuthorizeService playerAccountAuthorizeService
        )
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _playerAccountAuthorizeService = playerAccountAuthorizeService ??
                                             throw new ArgumentNullException(nameof(playerAccountAuthorizeService));
        }

        public AuthorizationFormPresenter Create(IAuthorizationFormView authorizationFormView)
        {
            return new AuthorizationFormPresenter(authorizationFormView, 
                _formService, _playerAccountAuthorizeService);
        }
    }
}