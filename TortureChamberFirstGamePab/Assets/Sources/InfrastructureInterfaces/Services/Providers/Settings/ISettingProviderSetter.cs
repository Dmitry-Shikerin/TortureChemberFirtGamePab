using Sources.Domain.Settings;

namespace Sources.InfrastructureInterfaces.Services.Providers.Settings
{
    public interface ISettingProviderSetter
    {
        void SetVolume(Volume volume);
    }
}