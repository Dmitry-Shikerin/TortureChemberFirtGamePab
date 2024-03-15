using System;
using Scripts.Presentation.UI;
using UnityEngine;

namespace Scripts.Presentation.Views.Player.Inventory
{
    public class PlayerInventorySlotView : MonoBehaviour
    {
        public ImageUI Image { get; private set; }
        public ImageUI BackgroundImage { get; private set; }

        public void Construct(ImageUI image, ImageUI backgroundImage)
        {
            Image = image ? image : throw new ArgumentNullException(nameof(image));
            BackgroundImage = backgroundImage
                ? backgroundImage
                : throw new ArgumentNullException(nameof(backgroundImage));
        }
    }
}