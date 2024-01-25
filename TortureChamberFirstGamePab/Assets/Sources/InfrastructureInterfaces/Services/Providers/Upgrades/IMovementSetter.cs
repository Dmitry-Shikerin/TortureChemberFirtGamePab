using Sources.Domain.Upgrades;

namespace Sources.InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IMovementSetter
    {
        void SetMovement(Upgrader movement);
    }
}