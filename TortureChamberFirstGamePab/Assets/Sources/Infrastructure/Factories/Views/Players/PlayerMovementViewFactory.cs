﻿using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;

namespace MyProject.Sources.Infrastructure.Factorys.Views
{
    public class PlayerMovementViewFactory
    {
        private readonly PlayerMovementPresenterFactory _playerMovementPresenterFactory;

        public PlayerMovementViewFactory
        (
            PlayerMovementPresenterFactory playerMovementPresenterFactory
        )
        {
            _playerMovementPresenterFactory = playerMovementPresenterFactory ??
                                              throw new ArgumentNullException(nameof(playerMovementPresenterFactory));
        }

        public IPlayerMovementView Create(PlayerMovement playerMovement, 
            PlayerMovementView playerMovementView, PlayerAnimation playerAnimation)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));
            if (playerMovementView == null) 
                throw new ArgumentNullException(nameof(playerMovementView));
            if (playerAnimation == null) 
                throw new ArgumentNullException(nameof(playerAnimation));

            PlayerMovementPresenter playerMovementPresenter =
                _playerMovementPresenterFactory.Create(playerMovement,
                    playerMovementView, playerAnimation);
            
            playerMovementView.Construct(playerMovementPresenter);

            return playerMovementView;
        }
    }
}