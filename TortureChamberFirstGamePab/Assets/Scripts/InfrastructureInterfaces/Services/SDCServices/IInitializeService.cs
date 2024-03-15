using Cysharp.Threading.Tasks;

namespace Scripts.InfrastructureInterfaces.Services.SDCServices
{
    public interface IInitializeService
    {
        void GameReady();
        void EnableCallbackLogging();
        UniTask Initialize();
    }
}