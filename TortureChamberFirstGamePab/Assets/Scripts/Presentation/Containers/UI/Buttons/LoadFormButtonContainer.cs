using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Buttons
{
    public class LoadFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI CloseButton { get; private set; }
    }
}