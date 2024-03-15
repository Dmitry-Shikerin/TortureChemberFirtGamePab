using Agava.YandexGames;
using Scripts.Domain.Constants;

namespace Scripts.Domain.YandexSDC
{
    public class LeaderboardPlayer
    {
        public LeaderboardPlayer(LeaderboardEntryResponse leaderboardEntryResponse)
        {
            Rank = leaderboardEntryResponse.rank;
            Name = leaderboardEntryResponse.player.publicName;

            if (string.IsNullOrEmpty(leaderboardEntryResponse.player.publicName))
                Name = LeaderboardName.AnonymousName;

            Score = leaderboardEntryResponse.score;
        }

        public LeaderboardPlayer(int rank, string name, int score)
        {
            Rank = rank;
            Name = name;
            Score = score;
        }

        public int Rank { get; }
        public string Name { get; }
        public int Score { get; }
    }
}