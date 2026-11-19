using Presentation.UI.Buttons;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.Buttons
{
    public class LoadFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI CloseButton { get; private set; }
    }
}