namespace Sources.InfrastructureInterfaces.Services.UpdateServices
{
    public interface ILateUpdatable
    {
        void UpdateLate(float deltaTime);
    }
}