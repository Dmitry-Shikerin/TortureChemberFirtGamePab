namespace Scripts.PresentationInterfaces.Views.YandexSDC
{
    public interface ILeaderboardElementView
    {
        void SetName(string playerName);
        void SetRank(string rank);
        void SetScore(string score);
    }
}