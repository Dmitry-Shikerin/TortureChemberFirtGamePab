using Sources.Domain.Settings;

namespace Sources.InfrastructureInterfaces.Services.Providers.Settings
{
    public interface ISettingProvider
    {
        Volume Volume { get; }
    }
}