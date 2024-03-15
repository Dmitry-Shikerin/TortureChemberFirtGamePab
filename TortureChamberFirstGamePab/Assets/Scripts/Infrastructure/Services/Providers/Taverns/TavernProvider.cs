using System;
using Scripts.Domain.GamePlays;
using Scripts.Domain.Taverns;
using Scripts.InfrastructureInterfaces.Services.Providers.Taverns;

namespace Scripts.Infrastructure.Services.Providers.Taverns
{
    public class TavernProvider : ITavernProvider, ITavernProviderSetter
    {
        public TavernMood TavernMood { get; private set; }
        public VisitorQuantity VisitorQuantity { get; private set; }

        public void SetTavernMood(TavernMood tavern) =>
            TavernMood = tavern ?? throw new ArgumentNullException(nameof(tavern));

        public void SetGameplay(VisitorQuantity visitorQuantity) =>
            VisitorQuantity = visitorQuantity ?? throw new ArgumentNullException(nameof(visitorQuantity));
    }
}