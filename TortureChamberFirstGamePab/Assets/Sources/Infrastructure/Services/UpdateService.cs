using System;
using Sources.InfrastructureInterfaces.Services;

namespace Sources.Infrastructure.Services
{
    public class UpdateService : IUpdateService
    {
        public event Action<float> ChangedUpdate;
        public event Action<float> ChangedFixedUpdate;
        public event Action<float> ChangedLateUpdate;


        public void Update(float deltaTime) => 
            ChangedUpdate?.Invoke(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) => 
            ChangedFixedUpdate?.Invoke(fixedDeltaTime);

        public void UpdateLate(float deltaTime) => 
            ChangedLateUpdate?.Invoke(deltaTime);
    }
}