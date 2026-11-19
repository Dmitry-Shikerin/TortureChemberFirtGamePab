using System;
using InfrastructureInterfaces.Services.PauseServices;
using InfrastructureInterfaces.Services.UpdateServices;
using InfrastructureInterfaces.Services.UpdateServices.Changer;

namespace Infrastructure.Services
{
    public class UpdateService : IUpdateService, IUpdateServiceChanger
    {
        private readonly IPauseService _pauseService;

        public UpdateService(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public event Action<float> ChangedUpdate;
        public event Action<float> ChangedLateUpdate;

        public void Update(float deltaTime)
        {
            if (_pauseService.IsPaused)
                return;

            ChangedUpdate?.Invoke(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
            if (_pauseService.IsPaused)
                return;

            ChangedLateUpdate?.Invoke(deltaTime);
        }
    }
}