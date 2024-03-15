using Scripts.Domain.Upgrades;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IMovementSetter
    {
        void SetMovement(Upgrader movement);
    }
}