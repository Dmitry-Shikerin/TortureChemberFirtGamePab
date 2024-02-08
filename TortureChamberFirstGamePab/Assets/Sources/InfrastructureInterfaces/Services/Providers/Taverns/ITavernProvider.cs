using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Infrastructure.Services.Providers.Taverns
{
    public interface ITavernProvider
    {
        TavernMood TavernMood { get; }
        VisitorQuantity VisitorQuantity { get; }
    }
}