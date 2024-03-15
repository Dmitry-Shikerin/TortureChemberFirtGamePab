using System;
using Scripts.Domain.YandexSDC;
using Scripts.PresentationInterfaces.Views.YandexSDC;

namespace Scripts.Controllers.YandexSDC
{
    public class LeaderboardElementPresenter : PresenterBase
    {
        private readonly ILeaderboardElementView _leaderboardElementView;
        private readonly LeaderboardPlayer _leaderboardPlayer;

        public LeaderboardElementPresenter(
            LeaderboardPlayer leaderboardPlayer,
            ILeaderboardElementView leaderboardElementView)
        {
            _leaderboardPlayer = leaderboardPlayer ?? throw new ArgumentNullException(nameof(leaderboardPlayer));
            _leaderboardElementView = leaderboardElementView ??
                                      throw new ArgumentNullException(nameof(leaderboardElementView));
        }

        public override void Enable()
        {
            _leaderboardElementView.SetName(_leaderboardPlayer.Name);
            _leaderboardElementView.SetRank(_leaderboardPlayer.Rank.ToString());
            _leaderboardElementView.SetScore(_leaderboardPlayer.Score.ToString());
        }
    }
}