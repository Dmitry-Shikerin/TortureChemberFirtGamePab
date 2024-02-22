﻿using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class LoadFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public LoadFormPresenterFactory
        (
            IFormService formService,
            IPauseService pauseService
        )
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public LoadFormPresenter Create(ILoadFormView pauseMenuFormView)
        {
            if (pauseMenuFormView == null) 
                throw new ArgumentNullException(nameof(pauseMenuFormView));
            
            return new LoadFormPresenter(pauseMenuFormView, _formService, _pauseService);
        }

    }
}