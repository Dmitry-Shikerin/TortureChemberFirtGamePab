using Cysharp.Threading.Tasks;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public interface IInitializeService
    {
        void GameReady();
        void Register();
        UniTask Initialize();
    }
}