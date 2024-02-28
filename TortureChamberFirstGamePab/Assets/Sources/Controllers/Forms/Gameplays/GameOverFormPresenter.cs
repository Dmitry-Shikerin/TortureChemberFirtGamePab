using System;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.Players;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;
using UnityEngine;

namespace Sources.Controllers.Forms.Gameplays
{
    public class GameOverFormPresenter : PresenterBase
    {
        private readonly IGameOverFormView _gameOverFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly IDataService<Domain.DataAccess.Containers.Players.Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IDataService<PlayerUpgrade> _upgradeDataService;
        private readonly IPlayerProvider _playerProvider;

        private PlayerWallet _playerWallet;
        
        public GameOverFormPresenter
        (
            IGameOverFormView gameOverFormView,
            IFormService formService,
            IPauseService pauseService,
            IDataService<Domain.DataAccess.Containers.Players.Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> upgradeDataService,
            IPlayerProvider playerProvider
        )
        {
            _gameOverFormView = gameOverFormView ?? throw new ArgumentNullException(nameof(gameOverFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _upgradeDataService = upgradeDataService ?? throw new ArgumentNullException(nameof(upgradeDataService));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;

        public override void Enable()
        {
            Debug.Log($"{nameof(GameOverFormPresenter)} Enable pause");
            
            _pauseService.Pause();
        }

        public override void Disable()
        {
            Debug.Log($"{nameof(GameOverFormPresenter)} Disable pause");
            
            _pauseService.Continue();
        }
    }
}