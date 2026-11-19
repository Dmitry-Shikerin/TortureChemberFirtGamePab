using System;
using InfrastructureInterfaces.Services.Forms;
using InfrastructureInterfaces.Services.PauseServices;
using Presentation.Views.Forms;
using Presentation.Views.Forms.Gameplays;

namespace Controllers.Forms.Gameplays
{
    public class PauseMenuFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public PauseMenuFormPresenter(
            IFormService formService,
            IPauseService pauseService)
        {
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

        public void ShowHudFormView()
        {
            _formService.Show<HudFormView>();
        }

        public void ShowTutorialFormView()
        {
            _formService.Show<TutorialFormView>();
        }

        public void ShowSettingsFormView()
        {
            _formService.Show<SettingFormView>();
        }
    }
}