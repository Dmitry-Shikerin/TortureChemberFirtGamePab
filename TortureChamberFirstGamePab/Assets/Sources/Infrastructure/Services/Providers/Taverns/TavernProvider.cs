using System;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Infrastructure.Services.Providers.Taverns
{
    public class TavernProvider : ITavernProvider, ITavernProviderSetter
    {
        public TavernMood TavernMood { get; private set; }
        public VisitorQuantity VisitorQuantity { get; private set; }

        public void SetTavernMood(TavernMood tavern)
        {
            TavernMood = tavern ?? throw new ArgumentNullException(nameof(tavern));
        }

        public void SetGameplay(VisitorQuantity visitorQuantity)
        {
            VisitorQuantity = visitorQuantity ?? throw new ArgumentNullException(nameof(visitorQuantity));
        }
    }
}