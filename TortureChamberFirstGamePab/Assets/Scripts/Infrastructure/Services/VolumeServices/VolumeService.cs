using System;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.Settings;
using Scripts.InfrastructureInterfaces.Services.VolumeServices;

namespace Scripts.Infrastructure.Services.VolumeServices
{
    public class VolumeService : IVolumeService
    {
        private readonly Volume _volume;

        public VolumeService(Setting setting)
        {
            _volume = setting.Volume ?? throw new NullReferenceException(nameof(setting.Volume));
        }

        public event Action VolumeChanged;

        public float Volume => _volume.VolumeValue;

        public void Enter(object payload = null)
        {
            OnVolumeChanged();

            _volume.VolumeChanged += OnVolumeChanged;
        }

        public void Exit() =>
            _volume.VolumeChanged -= OnVolumeChanged;

        private void OnVolumeChanged() =>
            VolumeChanged?.Invoke();
    }
}