using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Views.Visitors;
using UnityEngine;

namespace Sources.Presentation.Views.Visitors
{
    public class VisitorImageUIContainer : MonoBehaviour, IVisitorImageUI
    {
        [field: SerializeField] public ImageUI BackGroundImage { get; private set; }
        [field: SerializeField] public ImageUI OrderImage { get; private set; }
        [field: SerializeField] public Sprite EatSprite { get; private set; }
    }
}