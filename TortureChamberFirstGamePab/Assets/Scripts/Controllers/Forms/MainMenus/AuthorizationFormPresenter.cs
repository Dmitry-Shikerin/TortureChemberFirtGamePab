﻿using System;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.Presentation.Views.Forms.MainMenus;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;

namespace Scripts.Controllers.Forms.MainMenus
{
    public class AuthorizationFormPresenter : PresenterBase
    {
        private readonly IAuthorizationFormView _authorizationFormView;
        private readonly IFormService _formService;
        private readonly IPlayerAccountAuthorizeService _playerAccountAuthorizeService;

        public AuthorizationFormPresenter(
            IAuthorizationFormView authorizationFormView,
            IFormService formService,
            IPlayerAccountAuthorizeService playerAccountAuthorizeService)
        {
            _authorizationFormView = authorizationFormView ??
                                     throw new ArgumentNullException(nameof(authorizationFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _playerAccountAuthorizeService =
                playerAccountAuthorizeService ??
                throw new ArgumentNullException(nameof(playerAccountAuthorizeService));
        }

        public override void Enable()
        {
            _authorizationFormView.BackToMainMenuButton.AddClickListener(ShowMainMenuForm);
            _authorizationFormView.AuthorizationButton.AddClickListener(Authorize);
        }

        public override void Disable()
        {
            _authorizationFormView.BackToMainMenuButton.RemoveClickListener(ShowMainMenuForm);
            _authorizationFormView.AuthorizationButton.RemoveClickListener(Authorize);
        }

        private void ShowMainMenuForm()
        {
            _formService.Show<MainMenuFormView>();
        }

        private void Authorize()
        {
            _playerAccountAuthorizeService.Authorize(ShowMainMenuForm);
        }
    }
}