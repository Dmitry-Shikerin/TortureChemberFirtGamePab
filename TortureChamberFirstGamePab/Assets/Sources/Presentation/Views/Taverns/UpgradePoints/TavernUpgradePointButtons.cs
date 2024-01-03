using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointButtons : MonoBehaviour
    {
        [field: SerializeField] public ButtonUI CharismaButtonUI { get; private set; }
        [field: SerializeField] public ButtonUI InventoryButtonUI { get; private set; }
        [field: SerializeField] public ButtonUI MovementButtonUI { get; private set; }
    }
}