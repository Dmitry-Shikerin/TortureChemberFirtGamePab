using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;
using UnityEngine;

namespace Sources.Controllers.Forms.Gameplays
{
    public class LoadFormPresenter : PresenterBase
    {
        private readonly ILoadFormView _loadFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public LoadFormPresenter
        (
            ILoadFormView loadFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _loadFormView = loadFormView ?? throw new ArgumentNullException(nameof(loadFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable()
        {
            _pauseService.Pause();
        }

        public override void Disable()
        {
            _pauseService.Continue();
        }

        public void ShowHudForm() => 
            _formService.Show<HudFormView>();
    }
}