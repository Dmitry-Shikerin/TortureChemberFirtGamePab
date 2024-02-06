using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Domain.Datas.Taverns
{
    public class Tavern
    {
        public Tavern(TavernMood tavernMood, GamePlay gamePlay)
        {
            TavernMood = tavernMood;
            GamePlay = gamePlay;
        }
        
        public TavernMood TavernMood { get; }
        public GamePlay GamePlay { get; }
    }
}