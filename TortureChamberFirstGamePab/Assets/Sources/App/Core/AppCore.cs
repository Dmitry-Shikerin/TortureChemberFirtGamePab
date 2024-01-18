using System;
using Sources.InfrastructureInterfaces.Services.ScenServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.App.Core
{
    public class AppCore : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Awake() => 
            DontDestroyOnLoad(this);

        private async void Start() => 
            await _sceneService.ChangeSceneAsync(SceneManager.GetActiveScene().name, null);

        private void Update() => 
            _sceneService?.Update(Time.deltaTime);

        private void FixedUpdate() => 
            _sceneService?.UpdateFixed(Time.fixedDeltaTime);

        private void LateUpdate() => 
            _sceneService?.UpdateLate(Time.deltaTime);

        public void Construct(ISceneService sceneService) =>
            _sceneService = sceneService ?? 
                            throw new ArgumentNullException(nameof(sceneService));
    }
}