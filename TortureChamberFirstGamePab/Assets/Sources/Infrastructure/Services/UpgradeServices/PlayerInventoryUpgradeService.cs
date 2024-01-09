using System;
using JetBrains.Annotations;
using Sources.Domain.Players;
using Sources.PresentationInterfaces.UI;
using Sources.Utils.Exceptions;
using UnityEngine;

namespace Sources.Infrastructure.Services.UpgradeServices
{
    //TODO обьединить эти сервисы под интерфейс?
    public class PlayerInventoryUpgradeService
    {
        private readonly PlayerInventory _playerInventory;
        private readonly ITextUI _textUI;

        public PlayerInventoryUpgradeService(PlayerInventory playerInventory, ITextUI textUI)
        {
            _playerInventory = playerInventory ?? throw new ArgumentNullException(nameof(playerInventory));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
        }

        public void Upgrade()
        {
            try
            {
                //TODO улучшает больше чем нужно
                _playerInventory.AddInventoryCapacity();
                ChangeUpgradeLevelMessage();
            }
            //TODO сделать кастомный эксепшн
            catch (InventoryFullException exception)
            {
                //TODO сделать кнопку не активной
                Debug.Log(exception.Message);
            }
        }
        
        //TODO как сделать это более гибко?
        private void ChangeUpgradeLevelMessage()
        {
            if(_playerInventory.InventoryCapacity == 2)
                _textUI.SetText("1й уровень");
            if(_playerInventory.InventoryCapacity == 2)
                _textUI.SetText("2й уровень");
            // if(_tavernMood.AddedAmountMood > 0.24f && _tavernMood.AddedAmountMood < 0.26f)
            //     _textUI.SetText("3й уровень");
        }
    }
}