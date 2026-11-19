using System;
using Controllers.Forms.Gameplays;
using InfrastructureInterfaces.Services.PauseServices;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenterFactory
    {
        private readonly IPauseService _pauseService;

        public GameOverFormPresenterFactory(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public GameOverFormPresenter Create(IGameOverFormView gameOverFormView)
        {
            if (gameOverFormView == null)
                throw new ArgumentNullException(nameof(gameOverFormView));

            return new GameOverFormPresenter(_pauseService);
        }
    }
}