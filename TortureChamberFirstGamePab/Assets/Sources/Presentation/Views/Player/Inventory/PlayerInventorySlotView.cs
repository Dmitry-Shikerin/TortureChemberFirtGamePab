using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.Presentation.Views.Player.Inventory
{
    public class PlayerInventorySlotView : MonoBehaviour
    {
        [field : SerializeField] public ImageUI Image { get; private set; }
        [field : SerializeField] public ImageUI BackgroundImage { get; private set; }
    }
}