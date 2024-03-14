using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.StateMachines.StateMachineBase;
using Sources.InfrastructureInterfaces.Services.ScenServices;
using Zenject;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Services.SceneServices
{
    public class SceneService : ISceneService
    {
        private readonly List<Func<string, UniTask>> _enteringHandlers = new();
        private readonly List<Func<UniTask>> _exitingHandlers = new();
        private readonly IReadOnlyDictionary<string, Func<object, SceneContext, UniTask<IScene>>> _sceneFactories;

        private readonly StateMachine _stateMachine;

        public SceneService(IReadOnlyDictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneFactories)
        {
            _stateMachine = new StateMachine();
            _sceneFactories = sceneFactories ??
                              throw new ArgumentNullException(nameof(sceneFactories));

            var projectContext = Object.FindObjectOfType<ProjectContext>();
            projectContext.Container.BindInterfacesAndSelfTo<SceneService>().FromInstance(this).AsSingle();
        }

        public async UniTask ChangeSceneAsync(string sceneName, object payload = null)
        {
            if (_sceneFactories.TryGetValue(
                sceneName, out Func<object, SceneContext, UniTask<IScene>> sceneFactory) == false)
                throw new InvalidOperationException(nameof(sceneName));

            foreach (var enteringHandler in _enteringHandlers)
                await enteringHandler.Invoke(sceneName);

            var sceneContext = Object.FindObjectOfType<SceneContext>();

            var scene = await sceneFactory.Invoke(payload, sceneContext);

            _stateMachine.ChangeState(scene, payload);

            foreach (var exitingHandler in _exitingHandlers)
                await exitingHandler.Invoke();
        }

        public void Disable()
        {
            _stateMachine.Exit();
        }

        public void Update(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
            _stateMachine.UpdateFixed(fixedDeltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
            _stateMachine.UpdateLate(deltaTime);
        }

        public void AddBeforeSceneChangeHandler(Func<string, UniTask> handler)
        {
            _enteringHandlers.Add(handler);
        }

        public void AddAfterSceneChangeHandler(Func<UniTask> handler)
        {
            _exitingHandlers.Add(handler);
        }

        public void RemoveEnterHandler(Func<string, UniTask> handler)
        {
            _enteringHandlers.Remove(handler);
        }

        public void RemoveExitHandler(Func<UniTask> handler)
        {
            _exitingHandlers.Remove(handler);
        }
    }
}