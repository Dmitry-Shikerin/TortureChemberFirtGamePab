using Domain.GamePlays;
using Domain.Taverns;

namespace InfrastructureInterfaces.Services.Providers.Taverns
{
    public interface ITavernProvider
    {
        TavernMood TavernMood { get; }
        VisitorQuantity VisitorQuantity { get; }
    }
}