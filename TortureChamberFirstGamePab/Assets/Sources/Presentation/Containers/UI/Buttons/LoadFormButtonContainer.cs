using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.Conteiners.Buttons
{
    public class LoadFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI CloseButton { get; private set; }
    }
}