using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Infrastructure.Services.Providers.Taverns
{
    public interface ITavernProviderSetter
    {
        void SetTavernMood(TavernMood tavernMood);
        void SetGameplay(VisitorQuantity visitorQuantity);
    }
}