using System.Collections.Generic;
using UnityEngine;

namespace Sources.Domain.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "UpgradeConfig", 
        menuName = "Characteristics/UpgradeConfig", order = 51)]
    public class UpgradeConfig : ScriptableObject
    {
        [field: SerializeField] public float CurrentAmountUpgrade { get; private set; }
        [field: SerializeField] public float AddedAmountUpgrade { get; private set; }
        [field: SerializeField] public float MaximumAmountUpgrade { get; private set; }
        [field: SerializeField] public List<int> MoneyPerUpgrades { get; private set; }
    }
}