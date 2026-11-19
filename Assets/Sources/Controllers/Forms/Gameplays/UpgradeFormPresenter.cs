using System;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.PauseServices;
using Presentation.Views.Forms.Gameplays;

namespace Controllers.Forms.Gameplays
{
    public class UpgradeFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public UpgradeFormPresenter(
            IFormService formService,
            IPauseService pauseService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable() =>
            _pauseService.Pause();

        public override void Disable() =>
            _pauseService.Continue();

        public void ShowHudForm() =>
            _formService.Show<HudFormView>();
    }
}