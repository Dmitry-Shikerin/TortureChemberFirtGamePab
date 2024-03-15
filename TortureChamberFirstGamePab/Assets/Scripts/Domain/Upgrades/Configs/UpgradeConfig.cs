using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Domain.Upgrades.Configs
{
    [CreateAssetMenu(
        fileName = "UpgradeConfig",
        menuName = "Characteristics/UpgradeConfig",
        order = 51)]
    public class UpgradeConfig : ScriptableObject
    {
        [field: SerializeField] public float StartAmountUpgrade { get; private set; }
        [field: SerializeField] public float AddedAmountUpgrade { get; private set; }
        [field: SerializeField] public List<int> MoneyPerUpgrades { get; private set; }
    }
}