using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Domain.Datas.Taverns
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