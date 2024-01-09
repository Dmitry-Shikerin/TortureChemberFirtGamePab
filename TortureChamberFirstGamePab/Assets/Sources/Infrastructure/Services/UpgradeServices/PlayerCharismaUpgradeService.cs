using System;
using JetBrains.Annotations;
using Sources.Domain.Taverns;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Infrastructure.Services.UpgradeServices
{
    public class PlayerCharismaUpgradeService
    {
        private readonly TavernMood _tavernMood;
        private readonly ITextUI _textUI;

        //TODO возможно брать модели под интерфейсом
        public PlayerCharismaUpgradeService(TavernMood tavernMood, ITextUI textUI)
        {
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
        }
        
        public void Upgrade()
        {
            try
            {
                //TODO улучшает больше чем нужно
                _tavernMood.AddAmountMood();
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
            if(_tavernMood.AddedAmountMood > 0.1f && _tavernMood.AddedAmountMood < 0.16f)
                _textUI.SetText("1й уровень");
            if(_tavernMood.AddedAmountMood > 0.19f && _tavernMood.AddedAmountMood < 0.24f)
                _textUI.SetText("2й уровень");
            if(_tavernMood.AddedAmountMood > 0.24f && _tavernMood.AddedAmountMood < 0.26f)
                _textUI.SetText("3й уровень");
        }
    }
}