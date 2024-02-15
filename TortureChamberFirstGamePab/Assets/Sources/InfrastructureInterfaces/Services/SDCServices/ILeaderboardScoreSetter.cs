namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public interface ILeaderboardScoreSetter
    {
        void SetPlayerScore(int score);
    }
}