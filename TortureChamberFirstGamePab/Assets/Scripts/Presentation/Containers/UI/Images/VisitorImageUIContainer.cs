using Scripts.Presentation.UI;
using Scripts.PresentationInterfaces.Views.Visitors;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Images
{
    public class VisitorImageUIContainer : MonoBehaviour, IVisitorImageUI
    {
        [field: SerializeField] public Sprite EatSprite { get; private set; }
        [field: SerializeField] public ImageUI BackGroundImage { get; private set; }
        [field: SerializeField] public ImageUI OrderImage { get; private set; }
    }
}