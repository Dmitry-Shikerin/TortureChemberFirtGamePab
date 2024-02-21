using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Controllers.Forms.Gameplays
{
    public class TutorialFormPresenter : PresenterBase
    {
        private readonly ITutorialFormView _tutorialFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public TutorialFormPresenter
        (
            ITutorialFormView tutorialFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _tutorialFormView = tutorialFormView ?? throw new ArgumentNullException(nameof(tutorialFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable()
        {
            _pauseService.Pause();
            _tutorialFormView.DownScrollButton.AddClickListener(DownScroll);
            _tutorialFormView.UpScrollButton.AddClickListener(UpScroll);
        }

        public override void Disable()
        {
            _pauseService.Continue();
            _tutorialFormView.DownScrollButton.RemoveClickListener(DownScroll);
            _tutorialFormView.UpScrollButton.RemoveClickListener(UpScroll);
        }

        public void ShowPauseMenu() => 
            _formService.Show<PauseMenuFormView>();

        private void DownScroll()
        {
            _tutorialFormView.DownScroll(_tutorialFormView.ScrollStep);
        }

        private void UpScroll()
        {
            _tutorialFormView.UpScroll(_tutorialFormView.ScrollStep);
        }
    }
}