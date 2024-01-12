using System;
using JetBrains.Annotations;
using Sources.DomainInterfaces.Upgrades;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        private const float StartTavernMoodValue = 0.5f;
        private const float RemovedAmountMood = 0.05f;
        
        private readonly IUpgradeble _upgradeble;
        private ObservableProperty<float> _tavernMoodValue = 
            new ObservableProperty<float>(StartTavernMoodValue);

        public TavernMood(IUpgradeble upgradeble)
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
        }

        public IObservableProperty<float> TavernMoodValue => _tavernMoodValue;

        public void AddTavernMood() => 
            _tavernMoodValue.Value += _upgradeble.AddedAmountUpgrade;

        public void RemoveTavernMood() => 
            _tavernMoodValue.Value -= RemovedAmountMood;
    }
}