using Scripts.Domain.GamePlays;
using Scripts.Domain.Taverns;

namespace Scripts.Domain.DataAccess.Containers.Taverns
{
    public class Tavern
    {
        public Tavern(TavernMood tavernMood, VisitorQuantity visitorQuantity)
        {
            TavernMood = tavernMood;
            VisitorQuantity = visitorQuantity;
        }

        public TavernMood TavernMood { get; }
        public VisitorQuantity VisitorQuantity { get; }
    }
}