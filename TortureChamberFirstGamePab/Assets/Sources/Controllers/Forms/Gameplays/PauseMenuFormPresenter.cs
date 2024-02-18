using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms;
using Sources.Presentation.Views.Forms.Gameplays;

namespace Sources.Controllers.Forms.Gameplays
{
    public class PauseMenuFormPresenter : PresenterBase
    {
        private readonly IPauseMenuFormView _pauseMenuFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public PauseMenuFormPresenter
        (
            IPauseMenuFormView pauseMenuFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _pauseMenuFormView = pauseMenuFormView ?? throw new ArgumentNullException(nameof(pauseMenuFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable() => 
            _pauseService.Pause();

        public override void Disable() => 
            _pauseService.Continue();

        public void ShowHudFormView() => 
            _formService.Show<HudFormView>();

        public void ShowTutorialFormView() => 
            _formService.Show<TutorialFormView>();

        public void ShowSettingsFormView() => 
            _formService.Show<SettingFormView>();
    }
}