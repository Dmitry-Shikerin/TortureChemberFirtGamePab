using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.UI.Conteiners.MainMenu;
using Sources.Presentation.Voids;
using Sources.PresentationInterfaces.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _dataService;

        private bool _canLoad;

        public MainMenuSceneFactory(SceneService sceneService,
            IDataService<Player> dataService, PlayerUpgradeDataService playerUpgradeDataService)
        {
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        public async UniTask<IScene> Create(object payload)
        {
            HUD hud = Object.FindObjectOfType<HUD>(true);
            
            //ButtonFactories
            ButtonUIPresenterFactory buttonUIPresenterFactory = new ButtonUIPresenterFactory();
            ButtonUIFactory buttonUIFactory = new ButtonUIFactory(buttonUIPresenterFactory);

            //MainMenuButtons
            HudButtonUIContainer hudButtonUIContainer = hud.GetComponent<HudButtonUIContainer>();

            IButtonUI continueGameButton = buttonUIFactory.Create(
                hudButtonUIContainer.ContinueGameButton, LoadGamePlayScene);

            buttonUIFactory.Create(hudButtonUIContainer.NewGameButton, CreateGamePlayScene);
            // buttonUIFactory.Create(hudButtonUIContainer.OptionsButton,);

            return new MainMenuScene(hud, continueGameButton, _dataService);
        }

        private async void LoadGamePlayScene() =>
            await _sceneService.ChangeSceneAsync("GamePlay", new LoadServicePayload(true));
        
        private async void CreateGamePlayScene()
        {
            _dataService.Clear();
            
            await _sceneService.ChangeSceneAsync("GamePlay", new LoadServicePayload(false));
        }
    }
}