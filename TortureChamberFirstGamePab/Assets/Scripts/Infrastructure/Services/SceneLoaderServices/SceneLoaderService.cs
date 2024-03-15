using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure.Services.SceneLoaderServices
{
    public class SceneLoaderService
    {
        public async UniTask Load(string sceneName) =>
            await SceneManager.LoadSceneAsync(sceneName);
    }
}