using System;
using Sources.Domain.Constants;
using Sources.Infrastructure.Payloads;
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

        
        private async void Start()
        {
            await _sceneService.ChangeSceneAsync(Constant.SceneNames.MainMenu,
                new InitializeServicePayload(true));
        }

        private void Update() => 
            _sceneService?.Update(Time.deltaTime);

        private void FixedUpdate() => 
            _sceneService?.UpdateFixed(Time.fixedDeltaTime);

        private void LateUpdate() => 
            _sceneService?.UpdateLate(Time.deltaTime);

        private void OnDestroy() => 
            _sceneService.Disable();

        public void Construct(ISceneService sceneService) =>
            _sceneService = sceneService ?? 
                            throw new ArgumentNullException(nameof(sceneService));
    }
}