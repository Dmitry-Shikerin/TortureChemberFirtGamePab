using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices
{
    public interface IWebGlService : IEnterable
    {
        bool IsWebGl { get; }
    }
}