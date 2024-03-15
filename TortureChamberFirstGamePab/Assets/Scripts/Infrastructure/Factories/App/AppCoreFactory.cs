using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Scripts.App.Core;
using Scripts.ControllersInterfaces.Scenes;
using Scripts.Domain.Constants;
using Scripts.Infrastructure.Factories.Scenes;
using Scripts.Infrastructure.Services.SceneLoaderServices;
using Scripts.Infrastructure.Services.SceneServices;
using Scripts.Presentation.Views.Applications;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure.Factories.App
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            ProjectContext projectContext = Object.FindObjectOfType<ProjectContext>();
            CurtainView curtainView =
                Object.Instantiate(Resources.Load<CurtainView>(PrefabPath.Curtain)) ??
                throw new NullReferenceException(nameof(CurtainView));
            CurtainImageLoaderView curtainImageLoaderView =
                curtainView.GetComponent<CurtainImageLoaderView>();
            projectContext.Container.Bind<CurtainView>().FromInstance(curtainView);

            Dictionary<string, Func<object, SceneContext, UniTask<IScene>>> sceneStates =
                new Dictionary<string, Func<object, SceneContext, UniTask<IScene>>>();
            SceneService sceneService = new SceneService(sceneStates);

            sceneStates[SceneName.MainMenu] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<MainMenuSceneFactory>().Create(payload);
            sceneStates[SceneName.Gameplay] = (payload, sceneContext) =>
                sceneContext.Container.Resolve<GamePlaySceneFactory>().Create(payload);

            sceneService.AddBeforeSceneChangeHandler(async _ =>
            {
                curtainImageLoaderView.PlayTwist();
                await curtainView.ShowCurtain();
            });

            sceneService.AddBeforeSceneChangeHandler(async sceneName =>
                await new SceneLoaderService().Load(sceneName));

            sceneService.AddAfterSceneChangeHandler(async () =>
                await UniTask.Delay(TimeSpan.FromSeconds(CurtainConstant.CurtainDelay)));

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