﻿using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Domain.Upgrades.Configs;
using Scripts.DomainInterfaces.UI.AudioSourcesActivators;
using Scripts.DomainInterfaces.Upgrades;
using Scripts.Utils.ObservableProperties;
using Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces.Generic;

namespace Scripts.Domain.Upgrades
{
    public class Upgrader : IUpgradable, IAudioSourceActivator
    {
        private readonly ObservableProperty<int> _currentLevelUpgrade;
        private readonly float _startAmountUpgrade;

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

        public Upgrader(
            float startAmountUpgrade,
            float addedAmountUpgrade,
            int maximumLevel,
            int currentLevelUpgrade,
            IEnumerable<int> moneyPerUpgrades)
        {
            _startAmountUpgrade = startAmountUpgrade;
            AddedAmountUpgrade = addedAmountUpgrade;
            MaximumLevel = maximumLevel;
            _currentLevelUpgrade = new ObservableProperty<int>(currentLevelUpgrade);
            MoneyPerUpgrades = moneyPerUpgrades.ToList();
        }

        public event Action AudioSourceActivated;

        public float CurrentAmountUpgrade => _startAmountUpgrade + _currentLevelUpgrade.Value * AddedAmountUpgrade;
        public IObservableProperty<int> CurrentLevelUpgrade => _currentLevelUpgrade;
        public IReadOnlyList<int> MoneyPerUpgrades { get; }
        public float AddedAmountUpgrade { get; }
        public int MaximumLevel { get; }

        public void Upgrade()
        {
            if (_currentLevelUpgrade.Value >= MaximumLevel)
                throw new InvalidOperationException("Достигнут максимальный лимит улучшения");

            _currentLevelUpgrade.Value++;

            AudioSourceActivated?.Invoke();
        }
    }
}