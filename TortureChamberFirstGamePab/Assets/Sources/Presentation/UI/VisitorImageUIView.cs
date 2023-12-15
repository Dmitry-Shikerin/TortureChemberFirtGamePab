using UnityEngine;

namespace Sources.Presentation.UI
{
    public class VisitorImageUIView : MonoBehaviour
    {
        [field: SerializeField] public ImageUI BackGroundImage { get; private set; }
        [field: SerializeField] public ImageUI OrderImage { get; private set; }
    }
}