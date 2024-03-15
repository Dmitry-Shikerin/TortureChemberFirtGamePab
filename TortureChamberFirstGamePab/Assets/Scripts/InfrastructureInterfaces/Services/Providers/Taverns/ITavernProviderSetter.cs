using Scripts.Domain.GamePlays;
using Scripts.Domain.Taverns;

namespace Scripts.InfrastructureInterfaces.Services.Providers.Taverns
{
    public interface ITavernProviderSetter
    {
        void SetTavernMood(TavernMood tavernMood);
        void SetGameplay(VisitorQuantity visitorQuantity);
    }
}