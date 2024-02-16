using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public GameOverFormPresenterFactory
        (
            IFormService formService,
            IPauseService pauseService
        )
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public GameOverFormPresenter Create(IGameOverFormView gameOverFormView)
        {
            return new GameOverFormPresenter(gameOverFormView, _formService, _pauseService);
        }
    }
}