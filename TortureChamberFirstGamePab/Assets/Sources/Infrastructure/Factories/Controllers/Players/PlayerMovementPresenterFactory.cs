using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Animations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Infrastructure.Services;

namespace MyProject.Sources.Infrastructure.Factorys.Controllers
{
    public class PlayerMovementPresenterFactory
    {
        private readonly InputService _inputService;

        public PlayerMovementPresenterFactory
        (
            InputService inputService
        )
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
        }

        public PlayerMovementPresenter Create(PlayerMovement playerMovement,
            IPlayerMovementView playerMovementView, IPlayerAnimation playerAnimation)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));
            if (playerMovementView == null)
                throw new ArgumentNullException(nameof(playerMovementView));

            return new PlayerMovementPresenter
            (
                playerMovementView,
                playerAnimation,
                playerMovement,
                _inputService
            );
        }
    }
}