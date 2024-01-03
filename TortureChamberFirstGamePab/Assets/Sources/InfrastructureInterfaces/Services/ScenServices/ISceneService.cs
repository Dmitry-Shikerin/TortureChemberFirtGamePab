using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Factorys.Services
{
    public interface ISceneService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        UniTask ChangeSceneAsync(string sceneName, object payload);
    }
}