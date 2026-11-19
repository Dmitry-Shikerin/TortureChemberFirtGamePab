using Cysharp.Threading.Tasks;
using InfrastructureInterfaces.Services.UpdateServices;

namespace InfrastructureInterfaces.Services.ScenServices
{
    public interface ISceneService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        UniTask ChangeSceneAsync(string sceneName, object payload);
        void Disable();
    }
}