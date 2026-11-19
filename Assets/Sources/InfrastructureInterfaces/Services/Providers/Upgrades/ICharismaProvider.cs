using Domain.Upgrades;

namespace InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface ICharismaProvider
    {
        Upgrader Charisma { get; }
    }
}