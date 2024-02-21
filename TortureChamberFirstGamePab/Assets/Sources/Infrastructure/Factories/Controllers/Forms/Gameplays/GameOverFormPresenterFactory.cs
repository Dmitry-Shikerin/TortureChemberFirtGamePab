using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly IDataService<Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IDataService<PlayerUpgrade> _upgradeDataService;

        public GameOverFormPresenterFactory
        (
            IFormService formService,
            IPauseService pauseService,
            IDataService<Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> upgradeDataService
        )
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _upgradeDataService = upgradeDataService ?? throw new ArgumentNullException(nameof(upgradeDataService));
        }

        public GameOverFormPresenter Create(IGameOverFormView gameOverFormView)
        {
            return new GameOverFormPresenter(gameOverFormView, _formService, _pauseService,
                _playerDataService, _tavernDataService, _upgradeDataService);
        }
    }
}