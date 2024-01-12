using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Visitors;
using UnityEngine;

namespace Sources.Presentation.UI
{
    public class VisitorImageUIContainer : MonoBehaviour, IVisitorImageUI
    {
        [field: SerializeField] public ImageUI BackGroundImage { get; private set; }
        [field: SerializeField] public ImageUI OrderImage { get; private set; }
    }
}