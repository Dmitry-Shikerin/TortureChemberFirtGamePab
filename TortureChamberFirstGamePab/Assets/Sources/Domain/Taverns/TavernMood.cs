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
        private readonly IUpgradeble _upgradeble;
        private const float StartTavernMoodValue = 0.5f;
        
        private ObservableProperty<float> _tavernMoodValue = 
            new ObservableProperty<float>(StartTavernMoodValue);

        private float _removedAmountMood ;

        public TavernMood(IUpgradeble upgradeble) => _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));

        public IObservableProperty<float> TavernMoodValue => _tavernMoodValue;

        public void AddTavernMood() => 
            _tavernMoodValue.Value += _upgradeble.AddedAmountUpgrade;

        public void RemoveTavernMood() => 
            _tavernMoodValue.Value -= _removedAmountMood;
    }
}