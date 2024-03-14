using System;
using Sources.InfrastructureInterfaces.Services.PauseServices;

namespace Sources.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenter : PresenterBase
    {
        private readonly IPauseService _pauseService;

        public GameOverFormPresenter(IPauseService pauseService)
        {
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
    }
}