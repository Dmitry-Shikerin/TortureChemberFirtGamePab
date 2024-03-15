using System;
using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.DataAccess.Containers.Taverns;
using Scripts.Domain.Players;
using Scripts.Domain.Taverns;
using Scripts.InfrastructureInterfaces.Services;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.Providers.Players;
using Scripts.InfrastructureInterfaces.Services.Providers.Taverns;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.Presentation.Views.Forms.Gameplays;

namespace Scripts.Infrastructure.Services
{
    public class GameOverService : IGameOverService
    {
        private readonly IFormService _formService;
        private readonly ILeaderboardScoreSetter _leaderboardScoreSetter;
        private readonly IDataService<Player> _playerDataService;
        private readonly IPlayerProvider _playerProvider;
        private readonly IDataService<PlayerUpgrade> _playerUpgradeDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly ITavernProvider _tavernProvider;

        private PlayerWallet _playerWallet;
        private TavernMood _tavernMood;

        public GameOverService(
            ITavernProvider tavernProvider,
            IPlayerProvider playerProvider,
            IFormService formService,
            ILeaderboardScoreSetter leaderboardScoreSetter,
            IDataService<Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService)
        {
            _tavernProvider = tavernProvider ?? throw new ArgumentNullException(nameof(tavernProvider));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _leaderboardScoreSetter = leaderboardScoreSetter ??
                                      throw new ArgumentNullException(nameof(leaderboardScoreSetter));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _playerUpgradeDataService = playerUpgradeDataService ??
                                        throw new ArgumentNullException(nameof(playerUpgradeDataService));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;
        private TavernMood TavernMood => _tavernMood ??= _tavernProvider.TavernMood;

        public void Enter(object payload = null) =>
            TavernMood.TavernMoodOver += OnGameOver;

        public void Exit() =>
            TavernMood.TavernMoodOver -= OnGameOver;

        private void OnGameOver()
        {
            _leaderboardScoreSetter.SetPlayerScore(PlayerWallet.Score.GetValue);
            _formService.Show<GameOverFormView>();
            _playerDataService.Clear();
            _tavernDataService.Clear();
            _playerUpgradeDataService.Clear();
        }
    }
}