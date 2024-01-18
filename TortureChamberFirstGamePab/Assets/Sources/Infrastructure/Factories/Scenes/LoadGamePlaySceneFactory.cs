using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces;
using Sources.ControllersInterfaces.Scenes;
using Sources.InfrastructureInterfaces.Factories.Scenes;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class LoadGamePlaySceneFactory : ISceneFactory
    {
        public async UniTask<IScene> Create(object payload)
        {
            return new LoadGamePlayScene();
        }
    }
}