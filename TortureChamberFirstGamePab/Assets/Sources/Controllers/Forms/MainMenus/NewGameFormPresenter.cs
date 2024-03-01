using System;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Controllers.Forms.MainMenus
{
    public class NewGameFormPresenter : PresenterBase
    {
        private readonly INewGameFormView _newGameFormView;
        private readonly IFormService _formService;
        private readonly SceneService _sceneService;
        private readonly IDataService<Domain.DataAccess.Containers.Players.Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IDataService<PlayerUpgrade> _upgradeDataService;

        public NewGameFormPresenter
        (
            INewGameFormView newGameFormView,
            IFormService formService,
            SceneService sceneService,
            IDataService<Domain.DataAccess.Containers.Players.Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> upgradeDataService
        )
        {
            _newGameFormView = newGameFormView ?? throw new ArgumentNullException(nameof(newGameFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _sceneService = sceneService;
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _upgradeDataService = upgradeDataService ?? throw new ArgumentNullException(nameof(upgradeDataService));
        }

        public override void Enable()
        {
            _newGameFormView.NewGameButton.AddClickListener(CreateNewGame);
            _newGameFormView.MainMenuButton.AddClickListener(BackToMainMenu);
        }

        public override void Disable()
        {
            _newGameFormView.NewGameButton.RemoveClickListener(CreateNewGame);
            _newGameFormView.MainMenuButton.RemoveClickListener(BackToMainMenu);
        }

        private async void CreateNewGame()
        {
            _playerDataService.Clear();
            _tavernDataService.Clear();
            _upgradeDataService.Clear();

            await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                new LoadServicePayload(false));
        }

        private void BackToMainMenu()
        {
            _formService.Show<MainMenuFormView>();
        }
    }
}