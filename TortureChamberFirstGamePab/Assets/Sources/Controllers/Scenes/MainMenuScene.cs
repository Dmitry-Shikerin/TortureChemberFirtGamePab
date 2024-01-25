using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Presentation.Voids;
using Sources.PresentationInterfaces.UI;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly HUD _hud;
        private readonly IButtonUI _continueGameButton;
        private readonly IDataService<Domain.Players.Data.Player> _dataService;

        public MainMenuScene(HUD hud, IButtonUI continueGameButton, 
            IDataService<Domain.Players.Data.Player> dataService)
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _continueGameButton = continueGameButton ?? throw new ArgumentNullException(nameof(continueGameButton));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
            _hud.Show();

            if (_dataService.CanLoad == false) 
                _continueGameButton.Disable();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
        }


        public void UpdateFixed(float fixedDeltaTime)
        {
        }
    }
}