using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces;
using Sources.Infrastructure.Services.SceneService;
using Sources.InfrastructureInterfaces.Factorys.Scenes;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly SceneService _sceneService;

        public MainMenuSceneFactory(SceneService sceneService)
        {
            _sceneService = sceneService ?? 
                            throw new ArgumentNullException(nameof(sceneService));
        }
        
        public async UniTask<IScene> Create(object payload)
        {
            Debug.Log("Сцена создана");

            return new MainMenuScene();
        }
    }
}