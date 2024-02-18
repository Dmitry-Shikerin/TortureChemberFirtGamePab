using System;
using Sources.Domain.Players;
using Sources.Domain.Taverns;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Services
{
    public class GameOverService : IGameOverService
    {
        private readonly ITavernProvider _tavernProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IFormService _formService;
        private readonly ILeaderboardScoreSetter _leaderboardScoreSetter;

        private PlayerWallet _playerWallet;
        private TavernMood _tavernMood;

        public GameOverService
        (
            ITavernProvider tavernProvider,
            IPlayerProvider playerProvider,
            IFormService formService,
            ILeaderboardScoreSetter leaderboardScoreSetter
        )
        {
            _tavernProvider = tavernProvider ?? throw new ArgumentNullException(nameof(tavernProvider));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _leaderboardScoreSetter = leaderboardScoreSetter ??
                                      throw new ArgumentNullException(nameof(leaderboardScoreSetter));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;
        private TavernMood TavernMood => _tavernMood ??= _tavernProvider.TavernMood;

        public void Enter(object payload = null)
        {
            TavernMood.TavernMoodOver += OnGameOver;
        }

        public void Exit()
        {
            TavernMood.TavernMoodOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            _leaderboardScoreSetter.SetPlayerScore(PlayerWallet.Coins.GetValue);
            _formService.Show<GameOverFormView>();
        }
    }
}