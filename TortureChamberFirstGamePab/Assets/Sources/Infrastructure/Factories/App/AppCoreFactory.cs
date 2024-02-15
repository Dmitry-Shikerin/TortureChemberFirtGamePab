using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.App.Core;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.SceneLoaderServices;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Presentation.Views.Applications;
using Sources.Presentation.Views.Bootstrap;
using UnityEngine;
using Object = UnityEngine.Object;
using Zenject;

namespace Sources.Infrastructure.Factories.App
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            CurtainView curtainView =
                Object.Instantiate(Resources.Load<CurtainView>(Constant.PrefabPaths.Curtain)) ??
                throw new NullReferenceException(nameof(CurtainView));
            CurtainImageLoaderView curtainImageLoaderView =
                curtainView.GetComponent<CurtainImageLoaderView>();

            Dictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneStates =
                new Dictionary<string, Func<object, SceneContext, UniTask<IScene>>>();
            SceneService sceneService = new SceneService(sceneStates);

            sceneStates[Constant.SceneNames.MainMenu] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<MainMenuSceneFactory>().Create(payload);
            sceneStates[Constant.SceneNames.Gameplay] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<GamePlaySceneFactory>().Create(payload);

            sceneService.AddBeforeSceneChangeHandler(async sceneName =>
            {
                curtainImageLoaderView.PlayTwist();
                await curtainView.ShowCurtain();
            });
            
            sceneService.AddBeforeSceneChangeHandler(async sceneName =>
                await new SceneLoaderService().Load(sceneName));
            
            sceneService.AddAfterSceneChangeHandler(async () =>
                await UniTask.Delay(TimeSpan.FromSeconds(Constant.App.CurtainDelay)));
            
            sceneService.AddAfterSceneChangeHandler(async () =>
            {
                curtainImageLoaderView.StopTwist();
                await curtainView.HideCurtain();
            });

            appCore.Construct(sceneService);

            return appCore;
        }
    }
}