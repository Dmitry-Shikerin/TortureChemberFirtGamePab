using Sources.Domain.GamePlays;

namespace Sources.Domain.Taverns.Data
{
    public class Tavern
    {
        public Tavern(TavernMood tavernMood, GamePlay gamePlay)
        {
            TavernMood = tavernMood;
            GamePlay = gamePlay;
        }
        public TavernMood TavernMood { get; private set; }
        public GamePlay GamePlay { get; private set; }
    }
}