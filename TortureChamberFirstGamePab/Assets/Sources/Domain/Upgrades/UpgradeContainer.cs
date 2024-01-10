using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.DomainInterfaces.Upgrades;

namespace Sources.Domain.Upgrades
{
    public class UpgradeContainer : IUpgradeble
    {
        private readonly IEnumerable<int> _levelThresholds;
        private readonly int _addedAmountUpgrade;

        public UpgradeContainer(IEnumerable<int> levelThresholds,int currentAmountUpgrade, int addedAmountUpgrade, int maximumUpgradeAmount)
        {
            if (addedAmountUpgrade <= 0) 
                throw new ArgumentOutOfRangeException(nameof(addedAmountUpgrade));
            _levelThresholds = levelThresholds ?? throw new ArgumentNullException(nameof(levelThresholds));
            CurrentAmountUpgrade = currentAmountUpgrade;
            AddedAmountUpgrade = addedAmountUpgrade;
            MaximumUpgradeAmount = maximumUpgradeAmount;
        }

        public int CurrentAmountUpgrade { get; private set; }
        public int AddedAmountUpgrade { get; }
        public int MaximumUpgradeAmount { get; }

        //TODO правильно ли?
        public IReadOnlyCollection<int> LevelThresholds => (IReadOnlyCollection<int>)_levelThresholds;

        public void Upgrade()
        {
            if (CurrentAmountUpgrade >= MaximumUpgradeAmount)
                throw new InvalidOperationException();
            
            CurrentAmountUpgrade += AddedAmountUpgrade;
        }
    }
}