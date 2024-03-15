using Cysharp.Threading.Tasks;
using Scripts.ControllersInterfaces.Scenes;

namespace Scripts.InfrastructureInterfaces.Factories.Scenes
{
    public interface ISceneFactory
    {
        UniTask<IScene> Create(object payload);
    }
}