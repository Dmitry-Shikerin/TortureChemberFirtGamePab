using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;

namespace Sources.Infrastructure.Services.Providers.Taverns
{
    public interface ITavernProviderSetter
    {
        void SetTavern(TavernMood tavernMood);
        void SetGameplay(GamePlay gamePlay);
    }
}