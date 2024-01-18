using System;
using System.Collections.Generic;
using Sources.App.Core;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.SceneLoaderServices;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Views.Bootstrap;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.App
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            //TODO гдето здесь создать зависимость на стор сервис и прокинуть его в фабрики?
            
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            CurtainView curtainView =
                Object.Instantiate(Resources.Load<CurtainView>("Views/Bootstrap/CurtainView")) ??
                throw new NullReferenceException(nameof(CurtainView));

            Dictionary<string, ISceneFactory> sceneStates = new Dictionary<string, ISceneFactory>();
            SceneService sceneService = new SceneService(sceneStates);

            sceneStates["MainMenu"] = new MainMenuSceneFactory(sceneService);
            sceneStates["GamePlay"] = new GamePlaySceneFactory(sceneService);

            // sceneService.AddBeforeSceneChangeHandler(sceneName => curtainView.Show());
            sceneService.AddBeforeSceneChangeHandler(sceneName => 
                new SceneLoaderService().Load(sceneName));
            // sceneService.AddAfterSceneChangeHandler(() => UniTask.Delay(TimeSpan.FromSeconds(2)));
            // sceneService.AddAfterSceneChangeHandler(curtainView.Hide);

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}