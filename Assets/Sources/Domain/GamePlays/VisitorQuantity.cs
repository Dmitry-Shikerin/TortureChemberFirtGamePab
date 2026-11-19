using Domain.Constants;
using Domain.DataAccess.TavernData;

namespace Domain.GamePlays
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