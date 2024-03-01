using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Infrastructure.Factories.Controllers.Forms.MainMenus
{
    public class NewGameFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IDataService<PlayerUpgrade> _upgradeDataService;

        public NewGameFormPresenterFactory
        (
            IFormService formService,
            SceneService sceneService,
            IDataService<Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> upgradeDataService
        )
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _upgradeDataService = upgradeDataService ?? throw new ArgumentNullException(nameof(upgradeDataService));
        }

        public NewGameFormPresenter Create(INewGameFormView newGameFormView)
        {
            return new NewGameFormPresenter(newGameFormView, _formService, _sceneService,
                _playerDataService, _tavernDataService, _upgradeDataService);
        }
    }
}