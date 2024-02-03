using System;
using Sources.Domain.YandexSDC;
using Sources.PresentationInterfaces.Views.YandexSDC;

namespace Sources.Controllers.YandexSDC
{
    public class LeaderboardElementPresenter : PresenterBase
    {
        private readonly LeaderboardPlayer _leaderboardPlayer;
        private readonly ILeaderboardElementView _leaderboardElementView;

        public LeaderboardElementPresenter
        (
            LeaderboardPlayer leaderboardPlayer,
            ILeaderboardElementView leaderboardElementView
        )
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

        public override void Disable()
        {
        }
    }
}