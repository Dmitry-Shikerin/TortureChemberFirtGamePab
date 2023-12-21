using System;
using JetBrains.Annotations;
using Sources.ControllersInterfaces;
using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly IInputService _inputService;

        public GamePlayScene
        (
            IInputService inputService
        )
        {
            _inputService = inputService ?? 
                            throw new ArgumentNullException(nameof(inputService));
        }

        public void Update(float deltaTime)
        {
            _inputService.Update(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        public void Enter(object payload)
        {
        }

        public void Exit()
        {
        }
    }
}