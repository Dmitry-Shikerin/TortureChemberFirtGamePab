using System;
using System.Collections.Generic;
using Sources.App.Core;
using Sources.Domain.Constants;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
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
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();
            
            PlayerDataService playerDataService = new PlayerDataService();

            CurtainView curtainView =
                Object.Instantiate(Resources.Load<CurtainView>(Constant.PrefabPaths.Curtain)) ??
                throw new NullReferenceException(nameof(CurtainView));

            Dictionary<string, ISceneFactory> sceneStates = new Dictionary<string, ISceneFactory>();
            SceneService sceneService = new SceneService(sceneStates);

            sceneStates[Constant.SceneNames.MainMenu] = new MainMenuSceneFactory(
                sceneService, playerDataService);
            sceneStates[Constant.SceneNames.GamePlay] = new GamePlaySceneFactory(sceneService);

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