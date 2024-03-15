using UnityEngine;

namespace Scripts.Domain.Upgrades.Configs.Containers
{
    [CreateAssetMenu(
        fileName = "UpgradeConfigContainer",
        menuName = "Characteristics/UpgradeConfigContainer",
        order = 51)]
    public class UpgradeConfigContainer : ScriptableObject
    {
        [field: SerializeField] public UpgradeConfig Charisma { get; private set; }
        [field: SerializeField] public UpgradeConfig Inventory { get; private set; }
        [field: SerializeField] public UpgradeConfig Movement { get; private set; }
    }
}