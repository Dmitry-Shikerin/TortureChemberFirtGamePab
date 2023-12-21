using Cysharp.Threading.Tasks;
using Sources.ControllersInterfaces;

namespace Sources.InfrastructureInterfaces.Factorys.Scenes
{
    public interface ISceneFactory
    {
        UniTask<IScene> Create(object payload);
    }
}