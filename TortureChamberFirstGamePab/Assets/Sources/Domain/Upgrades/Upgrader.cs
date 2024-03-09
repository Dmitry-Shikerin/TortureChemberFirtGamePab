using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Domain.Upgrades.Configs;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Upgrades
{
    public class Upgrader : IUpgradeble, IAudioSourceActivator
    {
        private ObservableProperty<int> _currentLevelUpgrade;
        private float _startAmountUpgrade;
        
        public event Action AudioSourceActivated;

        public Upgrader(UpgradeConfig upgradeConfig)
        {
            if (upgradeConfig.StartAmountUpgrade <= 0)
                throw new ArgumentOutOfRangeException(nameof(upgradeConfig.StartAmountUpgrade));
            if (upgradeConfig.AddedAmountUpgrade <= 0)
                throw new ArgumentOutOfRangeException(nameof(upgradeConfig.AddedAmountUpgrade));

            _startAmountUpgrade = upgradeConfig.StartAmountUpgrade;
            AddedAmountUpgrade = upgradeConfig.AddedAmountUpgrade;
            MaximumLevel = upgradeConfig.MoneyPerUpgrades.Count;
            MoneyPerUpgrades = upgradeConfig.MoneyPerUpgrades;
            _currentLevelUpgrade = new ObservableProperty<int>();
        }

        public Upgrader
        (
            float startAmountUpgrade,
            float addedAmountUpgrade,
            int maximumLevel,
            int currentLevelUpgrade,
            IEnumerable<int> moneyPerUpgrades
        )
        {
            _startAmountUpgrade = startAmountUpgrade;
            AddedAmountUpgrade = addedAmountUpgrade;
            MaximumLevel = maximumLevel;
            _currentLevelUpgrade = new ObservableProperty<int>(currentLevelUpgrade);
            MoneyPerUpgrades = moneyPerUpgrades.ToList();
        }

        public float AddedAmountUpgrade { get; }
        public float CurrentAmountUpgrade => _startAmountUpgrade + _currentLevelUpgrade.Value * AddedAmountUpgrade;
        public IObservableProperty<int> CurrentLevelUpgrade => _currentLevelUpgrade;
        public int MaximumLevel { get; }
        public IReadOnlyList<int> MoneyPerUpgrades { get; }

        public void Upgrade()
        {
            if (_currentLevelUpgrade.Value >= MaximumLevel)
                throw new InvalidOperationException("Достигнут максимальный лимит улучшения");

            _currentLevelUpgrade.Value++;
            
            Debug.Log(CurrentAmountUpgrade);
            
            AudioSourceActivated?.Invoke();
        }
    }
}