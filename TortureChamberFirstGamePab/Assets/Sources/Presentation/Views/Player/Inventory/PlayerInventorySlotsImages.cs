using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.Presentation.Views.Player.Inventory
{
    public class PlayerInventorySlotsImages : View
    {
        [field: SerializeField] public ImageUI FirsSlotImage { get; private set; }
        [field: SerializeField] public ImageUI FirsSlotBackgroundImage { get; private set; }
        [field: SerializeField] public ImageUI SecondSlotImage { get; private set; }
        [field: SerializeField] public ImageUI SecondSlotBackgroundImage { get; private set; }
        [field: SerializeField] public ImageUI ThirdSlotImage { get; private set; }
        [field: SerializeField] public ImageUI ThirdSlotBackgroundImage { get; private set; }
    }
}