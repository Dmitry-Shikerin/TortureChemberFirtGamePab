using Sources.Domain.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface ICharismaProviderSetter
    {
        void SetCharisma(Upgrader inventory);
    }
}