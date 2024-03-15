using Scripts.Domain.Upgrades;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface ICharismaProvider
    {
        Upgrader Charisma { get; }
    }
}