using Scripts.Domain.Upgrades;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface ICharismaProviderSetter
    {
        void SetCharisma(Upgrader inventory);
    }
}