using System;
using MyProject.Sources.Domain.PlayerMovement;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Infrastructure.Services.UpgradeServices
{
    //TODO как организовать главное меню
    public class PlayerMovementUpgradeService
    {
        private readonly PlayerMovement _playerMovement;
        private readonly ITextUI _textUI;

        public PlayerMovementUpgradeService(PlayerMovement playerMovement, ITextUI textUI)
        {
            _playerMovement = playerMovement ?? throw new ArgumentNullException(nameof(playerMovement));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
        }

        public void Upgrade()
        {
            //TODO добавить сюда проверки на наличие монет
            try
            {
                //TODO улучшает больше чем нужно
                _playerMovement.AddMovementSpeed();
                ChangeUpgradeLevelMessage();
            }
            //TODO сделать кастомный эксепшн
            catch (InvalidOperationException exception)
            {
                //TODO сделать кнопку не активной
                Debug.Log(exception.Message);
            }
        }

        //TODO как сделать это более гибко?
        private void ChangeUpgradeLevelMessage()
        {
            if(_playerMovement.MovementSpeed > 1.25f && _playerMovement.MovementSpeed < 1.55f)
                _textUI.SetText("1й уровень");
            if(_playerMovement.MovementSpeed > 1.55f && _playerMovement.MovementSpeed < 1.88f)
                _textUI.SetText("2й уровень");
            if(_playerMovement.MovementSpeed > 1.88f && _playerMovement.MovementSpeed < 2f)
                _textUI.SetText("3й уровень");
        }
    }
}