using System;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.UIs;

namespace Sources.Infrastructure.Services
{
    public class PauseMenuService
    {
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;
        private readonly PauseMenuWindow _pauseMenuWindow;

        public PauseMenuService
        (
            IInputService inputService,
            PauseMenuWindow pauseMenuWindow,
            IPauseService pauseService
        )
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _pauseMenuWindow =
                pauseMenuWindow ? pauseMenuWindow : throw new ArgumentNullException(nameof(pauseMenuWindow));
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
            if (_pauseMenuWindow.gameObject.activeSelf == false)
            {
                _pauseMenuWindow.Show();
                _pauseService.Pause();
                return;
            }

            _pauseMenuWindow.Hide();
            _pauseService.Continue();
        }

        public void Show()
        {
            _pauseMenuWindow.Show();
        }

        public void Hide()
        {
            _pauseMenuWindow.Hide();
        }
    }
}