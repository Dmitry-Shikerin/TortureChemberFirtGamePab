using System;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.InputServices;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.Presentation.Containers.UI.Buttons;
using Scripts.Presentation.Views.Forms.Gameplays;

namespace Scripts.Infrastructure.Services
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

        public void Enter() =>
            _inputService.PauseButtonChanged += OnPauseButtonChanged;

        public void Exit() =>
            _inputService.PauseButtonChanged -= OnPauseButtonChanged;

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