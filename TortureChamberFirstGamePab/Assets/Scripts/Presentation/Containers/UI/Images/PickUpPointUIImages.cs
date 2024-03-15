using Scripts.Presentation.UI;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Images
{
    public class PickUpPointUIImages : MonoBehaviour
    {
        [field: SerializeField] public ImageUI Image { get; private set; }
        [field: SerializeField] public ImageUI BackgroundImage { get; private set; }
    }
}