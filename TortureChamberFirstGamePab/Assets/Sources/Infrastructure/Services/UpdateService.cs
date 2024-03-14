using System;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.UpdateServices.Changer;

namespace Sources.Infrastructure.Services
{
    public class UpdateService : IUpdateService, IUpdateServiceChanger
    {
        private readonly IPauseService _pauseService;

        public UpdateService(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public void Update(float deltaTime)
        {
            if (_pauseService.IsPaused)
                return;

            ChangedUpdate?.Invoke(deltaTime);
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
            if (_pauseService.IsPaused)
                return;

            ChangedFixedUpdate?.Invoke(fixedDeltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
            if (_pauseService.IsPaused)
                return;

            ChangedLateUpdate?.Invoke(deltaTime);
        }

        public event Action<float> ChangedUpdate;
        public event Action<float> ChangedFixedUpdate;
        public event Action<float> ChangedLateUpdate;
    }
}