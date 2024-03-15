using Scripts.Domain.GamePlays;
using Scripts.Domain.Taverns;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Taverns
{
    public interface ITavernProvider
    {
        TavernMood TavernMood { get; }
        VisitorQuantity VisitorQuantity { get; }
    }
}