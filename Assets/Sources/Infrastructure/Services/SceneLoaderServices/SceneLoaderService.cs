using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoaderServices
{
    public class SceneLoaderService
    {
        public async UniTask Load(string sceneName) =>
            await SceneManager.LoadSceneAsync(sceneName);
    }
}