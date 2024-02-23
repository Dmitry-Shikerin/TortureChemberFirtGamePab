using SimpleInputNamespace;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Containers.UI
{
    public class JoysticksContainer : View
    {
        [field: SerializeField] public Joystick Movement { get; private set; }
        [field: SerializeField] public Joystick Rotate { get; private set; }
    }
}