using System.Collections.Generic;
using Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces.Generic;

namespace Scripts.DomainInterfaces.Upgrades
{
    public interface IUpgradable
    {
        float CurrentAmountUpgrade { get; }
        float AddedAmountUpgrade { get; }
        IObservableProperty<int> CurrentLevelUpgrade { get; }
        int MaximumLevel { get; }
        IReadOnlyList<int> MoneyPerUpgrades { get; }

        void Upgrade();
    }
}