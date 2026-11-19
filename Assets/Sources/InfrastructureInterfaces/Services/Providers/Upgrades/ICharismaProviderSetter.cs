using Domain.Upgrades;

namespace InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface ICharismaProviderSetter
    {
        void SetCharisma(Upgrader inventory);
    }
}