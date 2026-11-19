using Presentation.UI;
using PresentationInterfaces.Views.Visitors;
using UnityEngine;

namespace Presentation.Containers.UI.Images
{
    public class VisitorImageUIContainer : MonoBehaviour, IVisitorImageUI
    {
        [field: SerializeField] public Sprite EatSprite { get; private set; }
        [field: SerializeField] public ImageUI BackGroundImage { get; private set; }
        [field: SerializeField] public ImageUI OrderImage { get; private set; }
    }
}