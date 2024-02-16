using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenter : PresenterBase
    {
        private readonly IGameOverFormView _gameOverFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public GameOverFormPresenter
        (
            IGameOverFormView gameOverFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _gameOverFormView = gameOverFormView ?? throw new ArgumentNullException(nameof(gameOverFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable() => 
            _pauseService.Pause();

        public override void Disable() => 
            _pauseService.Continue();
    }
}