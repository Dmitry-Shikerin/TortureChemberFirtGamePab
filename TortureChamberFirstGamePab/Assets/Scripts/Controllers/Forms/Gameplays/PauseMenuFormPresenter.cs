using System;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.Presentation.Views.Forms;
using Scripts.Presentation.Views.Forms.Gameplays;

namespace Scripts.Controllers.Forms.Gameplays
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