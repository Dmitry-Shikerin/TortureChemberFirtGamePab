using System;
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
            HUD hud = Object.FindObjectOfType<HUD>(true);
            
            //ButtonFactories
            ButtonUIPresenterFactory buttonUIPresenterFactory = new ButtonUIPresenterFactory();
            ButtonUIFactory buttonUIFactory = new ButtonUIFactory(buttonUIPresenterFactory);
            
            //MainMenuButtons
            HudButtonUIContainer hudButtonUIContainer = hud.GetComponent<HudButtonUIContainer>();

            // buttonUIFactory.Create(hudButtonUIContainer.ContinueGameButton, ChangeScene);
            buttonUIFactory.Create(hudButtonUIContainer.NewGameButton, MoveToGamePlayScene); 
            // buttonUIFactory.Create(hudButtonUIContainer.OptionsButton,);

            return new MainMenuScene();
            //TODO нужен ли сюда юнитаск иилд
        }

        private async void MoveToGamePlayScene() => 
            await _sceneService.ChangeSceneAsync("GamePlay");
    }
}