using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;

namespace Sources.Domain.Constants
{
    public static class Constant
    {
        public const float Epsilon = 0.01f;

        public const float MaximumAmountFillingImage = 1;
        public const float MinimumAmountFillingImage = 0;

        public const float StartTavernMoodValue = 0.5f;
        public const float RemovedAmountMood = 0.05f;
        public const float VisitorSpawnDelay = 5f;
        public const int MaximumVisitorsCapacity = 2;
    }
}