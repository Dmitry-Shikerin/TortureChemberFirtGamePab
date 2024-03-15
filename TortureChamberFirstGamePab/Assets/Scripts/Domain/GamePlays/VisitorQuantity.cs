using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.TavernData;

namespace Scripts.Domain.GamePlays
{
    public class VisitorQuantity
    {
        public VisitorQuantity()
            : this(VisitorConstant.MaximumQuantity)
        {
        }

        public VisitorQuantity(GameplayData gameplayData)
            : this(gameplayData.MaximumVisitorsCapacity)
        {
        }

        private VisitorQuantity(int maximumVisitorsQuantity)
        {
            MaximumVisitorsQuantity = maximumVisitorsQuantity;
        }

        public int MaximumVisitorsQuantity { get; private set; }

        public void AddMaximumVisitorsQuantity()
        {
            MaximumVisitorsQuantity++;
        }
    }
}