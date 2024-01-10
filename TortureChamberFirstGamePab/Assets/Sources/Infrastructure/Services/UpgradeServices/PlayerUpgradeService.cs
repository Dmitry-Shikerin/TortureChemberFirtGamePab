using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Upgrades;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Infrastructure.Services.UpgradeServices
{
    public class PlayerUpgradeService
    {
        private readonly IUpgradeble _upgradeble;
        private readonly ITextUI _textUI;
        // private readonly IEnumerable<int> _levelThresholds;

        //TODO возможно брать модели под интерфейсом
        public PlayerUpgradeService(IUpgradeble upgradeble, ITextUI textUI)
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            // _levelThresholds = levelThresholds ?? throw new ArgumentNullException(nameof(levelThresholds));
        }
        
        public void Upgrade()
        {
            try
            {
                //TODO улучшает больше чем нужно
                _upgradeble.Upgrade();
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
            // if(_tavernMood.AddedAmountMood > 0.1f && _tavernMood.AddedAmountMood < 0.16f)
            //     _textUI.SetText("1й уровень");
            // if(_tavernMood.AddedAmountMood > 0.19f && _tavernMood.AddedAmountMood < 0.24f)
            //     _textUI.SetText("2й уровень");
            // if(_tavernMood.AddedAmountMood > 0.24f && _tavernMood.AddedAmountMood < 0.26f)
            //     _textUI.SetText("3й уровень");

            int currentLevel = 0;
            
            foreach (int levelThreshold in _upgradeble.LevelThresholds)
            {
                currentLevel++;

                if (_upgradeble.AddedAmountUpgrade > levelThreshold)
                {
                    _textUI.SetText($"{currentLevel} уровень");
                }
            }
        }
    }
}