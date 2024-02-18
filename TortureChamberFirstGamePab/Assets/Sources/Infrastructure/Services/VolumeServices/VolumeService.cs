using System;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.VolumeServices
{
    public class VolumeService : IVolumeService
    {
        private readonly Volume _volume;
        
        public VolumeService(Setting setting)
        {
            _volume = setting.Volume ?? throw new NullReferenceException(nameof(setting.Volume));
        }

        public void Enter(object payload = null)
        {
            OnVolumeChanged();
            
            _volume.VolumeChanged += OnVolumeChanged;
        }

        public void Exit()
        {
            _volume.VolumeChanged -= OnVolumeChanged;
        }

        private void OnVolumeChanged()
        {
            AudioListener.volume = _volume.VolumeValue;
            Debug.Log($"Громкость аудиолистенера {AudioListener.volume}");
        }
    }
}