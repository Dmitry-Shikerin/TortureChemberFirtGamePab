using System;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.Providers.Settings;

namespace Sources.Infrastructure.Services.Providers.Settings
{
    public class SettingProvider : ISettingProvider, ISettingProviderSetter
    {
        public Volume Volume { get; private set; }
        
        public void SetVolume(Volume volume) => 
            Volume = volume ?? throw new ArgumentNullException(nameof(volume));
    }
}