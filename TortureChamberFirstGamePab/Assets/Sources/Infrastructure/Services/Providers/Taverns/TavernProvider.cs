using System;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Infrastructure.Services.Providers.Taverns
{
    public class TavernProvider : ITavernProvider, ITavernProviderSetter
    {
        public TavernMood TavernMood { get; private set; }
        public GamePlay GamePlay { get; private set; }

        public void SetTavernMood(TavernMood tavern)
        {
            TavernMood = tavern ?? throw new ArgumentNullException(nameof(tavern));
        }

        public void SetGameplay(GamePlay gamePlay)
        {
            GamePlay = gamePlay ?? throw new ArgumentNullException(nameof(gamePlay));
        }
    }
}