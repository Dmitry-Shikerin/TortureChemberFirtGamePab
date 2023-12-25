using System;
using JetBrains.Annotations;
using Sources.ControllersInterfaces;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factories.Services;
using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;

        public GamePlayScene
        (
            IInputService inputService,
            IUpdateService updateService
        )
        {
            _inputService = inputService ?? 
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public void Update(float deltaTime)
        {
            _inputService.Update(deltaTime);
            _updateService?.Update(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
            _inputService.UpdateLate(deltaTime);
            _updateService.UpdateLate(deltaTime);
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
            _inputService.UpdateFixed(fixedDeltaTime);
            _updateService.UpdateFixed(fixedDeltaTime);
        }

        public void Enter(object payload)
        {
        }

        public void Exit()
        {
        }
    }
}