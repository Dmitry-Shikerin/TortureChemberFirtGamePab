using Presentation.Views;
using SimpleInputNamespace;
using UnityEngine;

namespace Presentation.Containers.UI
{
    public class JoysticksContainer : View
    {
        [field: SerializeField] public Joystick Movement { get; private set; }
        [field: SerializeField] public Joystick Rotate { get; private set; }
    }
}