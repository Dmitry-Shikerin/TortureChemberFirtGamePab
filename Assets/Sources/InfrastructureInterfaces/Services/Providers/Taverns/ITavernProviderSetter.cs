using Domain.GamePlays;
using Domain.Taverns;

namespace InfrastructureInterfaces.Services.Providers.Taverns
{
    public interface ITavernProviderSetter
    {
        void SetTavernMood(TavernMood tavernMood);
        void SetGameplay(VisitorQuantity visitorQuantity);
    }
}