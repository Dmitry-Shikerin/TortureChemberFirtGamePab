using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Domain.Upgrades.Configs;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Upgrades
{
    public class Upgrader : IUpgradeble
    {
        private ObservableProperty<int> _currentLevelUpgrade;
        private float _currentAmountUpgrade;

        public Upgrader(UpgradeConfig upgradeConfig)
        {
            if (upgradeConfig.CurrentAmountUpgrade <= 0)
                throw new ArgumentOutOfRangeException(nameof(upgradeConfig.CurrentAmountUpgrade));
            if (upgradeConfig.AddedAmountUpgrade <= 0)
                throw new ArgumentOutOfRangeException(nameof(upgradeConfig.AddedAmountUpgrade));
            if (upgradeConfig.MaximumAmountUpgrade <= 0)
                throw new ArgumentOutOfRangeException(nameof(upgradeConfig.MaximumAmountUpgrade));

            _currentAmountUpgrade = upgradeConfig.CurrentAmountUpgrade;
            AddedAmountUpgrade = upgradeConfig.AddedAmountUpgrade;
            MaximumUpgradeAmount = upgradeConfig.MaximumAmountUpgrade;
            //TODO тут есть статичные данные и статичные списки
            MoneyPerUpgrades = upgradeConfig.MoneyPerUpgrades;
            _currentLevelUpgrade = new ObservableProperty<int>();
        }

        public Upgrader
        (
            float currentAmountUpgrade,
            float addedAmountUpgrade,
            float maximumUpgradeAmount,
            int currentLevelUpgrade,
            IEnumerable<int> moneyPerUpgrades
        )
        {
            _currentAmountUpgrade = currentAmountUpgrade;
            AddedAmountUpgrade = addedAmountUpgrade;
            MaximumUpgradeAmount = maximumUpgradeAmount;
            _currentLevelUpgrade = new ObservableProperty<int>(currentLevelUpgrade);
            MoneyPerUpgrades = moneyPerUpgrades.ToList();
        }

        public float AddedAmountUpgrade { get; }
        public float CurrentAmountUpgrade => _currentAmountUpgrade;
        public IObservableProperty<int> CurrentLevelUpgrade => _currentLevelUpgrade;
        public float MaximumUpgradeAmount { get; }
        public IReadOnlyList<int> MoneyPerUpgrades { get; }

        public void Upgrade()
        {
            if (_currentAmountUpgrade >= MaximumUpgradeAmount)
                throw new InvalidOperationException("Достигнут максимальный лимит улучшения");

            _currentAmountUpgrade += AddedAmountUpgrade;
            Debug.Log(_currentAmountUpgrade);
            _currentLevelUpgrade.Value++;
        }
    }
}