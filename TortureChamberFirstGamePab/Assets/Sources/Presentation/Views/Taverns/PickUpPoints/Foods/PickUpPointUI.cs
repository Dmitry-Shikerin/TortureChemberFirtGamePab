using UnityEngine;

namespace Sources.Presentation.UI.PickUpPointUIs
{
    public class PickUpPointUI : MonoBehaviour
    {
        [field : SerializeField] public ImageUI Image { get; private set; }
        [field : SerializeField] public ImageUI BackgroundImage { get; private set; }
    }
}