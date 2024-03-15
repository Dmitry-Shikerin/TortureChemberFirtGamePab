using Scripts.Presentation.Views.Player.Upgardes;
using UnityEngine;

namespace Scripts.Presentation.Views.Taverns.UpgradePoints
{
    public class PlayerUpgradeViewsContainer : MonoBehaviour
    {
        [field: SerializeField] public PlayerUpgradeView CharismaUpgradeView { get; private set; }
        [field: SerializeField] public PlayerUpgradeView InventoryUpgradeView { get; private set; }
        [field: SerializeField] public PlayerUpgradeView MovementUpgradeView { get; private set; }
    }
}