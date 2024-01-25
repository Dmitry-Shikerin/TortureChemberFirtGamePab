using Sources.Domain.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface ICharismaProvider
    {
        Upgrader Charisma { get; }
    }
}