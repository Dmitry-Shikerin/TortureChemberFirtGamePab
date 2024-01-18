using System;
using Sources.Presentation.Views.UIs;

namespace Sources.Infrastructure.Services
{
    public class PauseMenuService
    {
        private readonly InputService _inputService;
        private readonly PauseMenuWindow _pauseMenuWindow;

        public PauseMenuService(InputService inputService, PauseMenuWindow pauseMenuWindow)
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
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
                //TODO здесь поставить на паузу
                return;
            }

            _pauseMenuWindow.Hide();
            //TODO здесь убрать с паузы
        }
    }
}