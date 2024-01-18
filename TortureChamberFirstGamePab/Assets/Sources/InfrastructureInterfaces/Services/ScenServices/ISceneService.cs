using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services.UpdateServices;

namespace Sources.InfrastructureInterfaces.Services.ScenServices
{
    public interface ISceneService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        UniTask ChangeSceneAsync(string sceneName, object payload);
    }
}