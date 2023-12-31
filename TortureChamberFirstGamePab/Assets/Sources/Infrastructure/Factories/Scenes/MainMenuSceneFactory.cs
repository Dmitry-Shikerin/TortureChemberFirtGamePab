﻿using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.SceneService;
using Sources.InfrastructureInterfaces.Factorys.Scenes;
using Sources.Presentation.UI.Conteiners.MainMenu;
using UnityEngine;
using Object = UnityEngine.Object;

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
            //TODO перекинуть все контейнеры в паку ЮАЙ
            Debug.Log("Сцена создана");
            //TODO потом убрать тру
            HUD hud = Object.FindObjectOfType<HUD>(true);
            
            //ButtonFactories
            ButtonUIPresenterFactory buttonUIPresenterFactory = new ButtonUIPresenterFactory();
            ButtonUIFactory buttonUIFactory = new ButtonUIFactory(buttonUIPresenterFactory);
            
            //MainMenuButtons
            HudButtonUIContainer hudButtonUIContainer = hud.GetComponent<HudButtonUIContainer>();

            // buttonUIFactory.Create(hudButtonUIContainer.ContinueGameButton, ChangeScene);
            buttonUIFactory.Create(hudButtonUIContainer.NewGameButton, ChangeScene); 
            // buttonUIFactory.Create(hudButtonUIContainer.OptionsButton,);

            return new MainMenuScene();
        }

        //TODO не придумал как сделать по другому
        private async void ChangeScene() => 
            await _sceneService.ChangeSceneAsync("GamePlay");
    }
}