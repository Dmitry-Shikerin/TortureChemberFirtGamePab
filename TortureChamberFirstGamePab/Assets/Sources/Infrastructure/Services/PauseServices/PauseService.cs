using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services.PauseServices;

namespace Sources.Infrastructure.Services.PauseServices
{
    public class PauseService : IPauseService
    {
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Continue()
        {
            IsPaused = false;
        }
        
        public async UniTask OnPauseAsync()
        {
            while (IsPaused)
            {
                await UniTask.Yield();
            }
        }
    }
}