using Domain.Upgrades;

namespace InfrastructureInterfaces.Services.Providers.Upgrades
{
    public interface IMovementSetter
    {
        void SetMovement(Upgrader movement);
    }
}