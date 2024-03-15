using Cysharp.Threading.Tasks;
using Scripts.InfrastructureInterfaces.Services.UpdateServices;

namespace Scripts.InfrastructureInterfaces.Services.ScenServices
{
    public interface ISceneService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        UniTask ChangeSceneAsync(string sceneName, object payload);
        void Disable();
    }
}