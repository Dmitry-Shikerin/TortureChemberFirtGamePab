using Domain.GamePlays;
using Domain.Taverns;

namespace Domain.DataAccess.Containers.Taverns
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