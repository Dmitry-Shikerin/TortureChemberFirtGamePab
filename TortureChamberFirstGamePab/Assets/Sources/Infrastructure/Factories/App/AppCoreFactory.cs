using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.App.Core;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.SceneLoaderServices;
using Sources.Infrastructure.Services.SceneServices;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Factories.App
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();
            
            
            
            //TODO исправить курточку загораживает рейкасты
            // CurtainView curtainView =
            //     Object.Instantiate(Resources.Load<CurtainView>(Constant.PrefabPaths.Curtain)) ??
            //     throw new NullReferenceException(nameof(CurtainView));

            Dictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneStates =
                new Dictionary<string, Func<object, SceneContext, UniTask<IScene>>>();
            SceneService sceneService = new SceneService(sceneStates);

            sceneStates[Constant.SceneNames.MainMenu] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<MainMenuSceneFactory>().Create(payload);
            sceneStates[Constant.SceneNames.Gameplay] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<GamePlaySceneFactory>().Create(payload);

            //Todo сделать курточку здесь
            //TODO возможно сделать крутилку
            // sceneService.AddBeforeSceneChangeHandler(sceneName => curtainView.Show());
            sceneService.AddBeforeSceneChangeHandler(async sceneName =>
                await new SceneLoaderService().Load(sceneName));
            // sceneService.AddAfterSceneChangeHandler(() => UniTask.Delay(TimeSpan.FromSeconds(2)));
            // sceneService.AddAfterSceneChangeHandler(curtainView.Hide);

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}