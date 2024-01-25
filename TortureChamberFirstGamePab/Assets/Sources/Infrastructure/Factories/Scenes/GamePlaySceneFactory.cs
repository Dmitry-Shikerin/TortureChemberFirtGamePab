using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Zenject;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GamePlaySceneFactory : ISceneFactory
    {
        private readonly SceneService _sceneService;
        private SceneContext _sceneContext;
        
        public GamePlaySceneFactory(SceneService sceneService)
        {
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
        }

        public async UniTask<IScene> Create(object payload)
        {
            _sceneContext = Object.FindObjectOfType<SceneContext>();
            
            bool canLoad = payload is LoadServicePayload { CanLoad: true };

            ILoadService loadService = CreateLoadService(canLoad);
        
            return new GamePlayScene
            (
                Resolve<IInputService>(),
                Resolve<UpdateService>(),
                Resolve<VisitorSpawnService>(),
                Resolve<TavernUpgradePointService>(),
                Resolve<GamePlayService>(),
                Resolve<PauseMenuService>(),
                loadService
            );
        }

        private ILoadService CreateLoadService(bool canLoad)
        {
            if (canLoad == false)
                return Resolve<CreateService>();

            return Resolve<LoadService>();
        }

        //TODO можно ли так?
        private T Resolve<T>() => 
            _sceneContext.Container.Resolve<T>();
    }
}