using ControllersInterfaces.Scenes;
using Cysharp.Threading.Tasks;

namespace InfrastructureInterfaces.Factories.Scenes
{
    public interface ISceneFactory
    {
        UniTask<IScene> Create(object payload);
    }
}