namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public interface ILeaderboardInitializeService
    {
        void SetPlayerScore(int score);
        void Fill();
    }
}