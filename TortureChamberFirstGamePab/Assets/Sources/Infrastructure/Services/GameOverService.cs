using System;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.Players;
using Sources.Domain.Taverns;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.Presentation.Views.Forms.Gameplays;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class GameOverService : IGameOverService
    {
        private readonly ITavernProvider _tavernProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IFormService _formService;
        private readonly ILeaderboardScoreSetter _leaderboardScoreSetter;
        private readonly IDataService<Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDataService;
        private readonly IDataService<PlayerUpgrade> _playerUpgradeDataService;

        private PlayerWallet _playerWallet;
        private TavernMood _tavernMood;

        public GameOverService
        (
            ITavernProvider tavernProvider,
            IPlayerProvider playerProvider,
            IFormService formService,
            ILeaderboardScoreSetter leaderboardScoreSetter,
            IDataService<Player> playerDataService,
            IDataService<Tavern> tavernDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService
        )
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

        //TODO проверить все локализации
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