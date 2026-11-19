using Cysharp.Threading.Tasks;

namespace InfrastructureInterfaces.Services.SDCServices
{
    public interface IInitializeService
    {
        void GameReady();
        void EnableCallbackLogging();
        UniTask Initialize();
    }
}