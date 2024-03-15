using Scripts.Presentation.UI.Buttons;
using UnityEngine;

namespace Scripts.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointButtons : MonoBehaviour
    {
        [field: SerializeField] public ButtonUI CharismaButtonUI { get; private set; }
        [field: SerializeField] public ButtonUI InventoryButtonUI { get; private set; }
        [field: SerializeField] public ButtonUI MovementButtonUI { get; private set; }
        [field: SerializeField] public ButtonUI AdvertisementButtonUI { get; private set; }
        [field: SerializeField] public ButtonUI CloseButtonUI { get; private set; }
    }
}