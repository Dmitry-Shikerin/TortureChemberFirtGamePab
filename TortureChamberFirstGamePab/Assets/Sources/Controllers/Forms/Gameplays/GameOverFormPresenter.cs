using System;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenter : PresenterBase
    {
        private readonly IGameOverFormView _gameOverFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly IDataService<Domain.Datas.Players.Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IDataService<PlayerUpgrade> _upgradeDataService;

        public GameOverFormPresenter
        (
            IGameOverFormView gameOverFormView,
            IFormService formService,
            IPauseService pauseService,
            IDataService<Domain.Datas.Players.Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> upgradeDataService
            )
        {
            _gameOverFormView = gameOverFormView ?? throw new ArgumentNullException(nameof(gameOverFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _upgradeDataService = upgradeDataService ?? throw new ArgumentNullException(nameof(upgradeDataService));
        }

        public override void Enable()
        {
            _playerDataService.Clear();
            _tavernDataService.Clear();
            _upgradeDataService.Clear();
            
            _pauseService.Pause();
        }

        public override void Disable() => 
            _pauseService.Continue();
    }
}