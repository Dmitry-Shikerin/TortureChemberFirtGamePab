using System.Collections.Generic;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;

namespace Sources.DomainInterfaces.Upgrades
{
    public interface IUpgradeble
    {
        float CurrentAmountUpgrade { get; }
        float AddedAmountUpgrade { get; }
        IObservableProperty<int> CurrentLevelUpgrade { get; }
        float MaximumUpgradeAmount { get; }
        IReadOnlyList<int> MoneyPerUpgrades { get; }
        
        void Upgrade();
    }
}