using Presentation.UI;
using UnityEngine;

namespace Presentation.Containers.UI.Images
{
    public class PickUpPointUIImages : MonoBehaviour
    {
        [field: SerializeField] public ImageUI Image { get; private set; }
        [field: SerializeField] public ImageUI BackgroundImage { get; private set; }
    }
}