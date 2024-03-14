using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.Presentation.Views.UIs;

namespace Sources.Infrastructure.Services
{
    public class PauseMenuService
    {
        private readonly IFormService _formService;
        private readonly IInputService _inputService;
        private readonly PauseMenuButtonContainer _pauseMenuButtonContainer;
        private readonly IPauseService _pauseService;

        public PauseMenuService(
            IInputService inputService,
            IFormService formService,
            IPauseService pauseService,
            PauseMenuButtonContainer pauseMenuButtonContainer)
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _pauseMenuButtonContainer = pauseMenuButtonContainer
                ? pauseMenuButtonContainer
                : throw new ArgumentNullException(nameof(pauseMenuButtonContainer));
        }

        public void Enter()
        {
            _inputService.PauseButtonChanged += OnPauseButtonChanged;
        }

        public void Exit()
        {
            _inputService.PauseButtonChanged -= OnPauseButtonChanged;
        }

        private void OnPauseButtonChanged()
        {
            if (_pauseMenuButtonContainer.gameObject.activeSelf == false)
            {
                _formService.Show<PauseMenuFormView>();
                _pauseService.Pause();
                return;
            }

            _formService.Show<HudFormView>();
            _pauseService.Continue();
        }
    }
}