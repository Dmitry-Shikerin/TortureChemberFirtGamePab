using System;
using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.Presentation.Views.Player.Inventory
{
    public class PlayerInventorySlotView : MonoBehaviour
    {
        // [field : SerializeField] public ImageUI Image { get; private set; }
        // [field : SerializeField] public ImageUI BackgroundImage { get; private set; }
        public ImageUI Image { get; private set; }
        public ImageUI BackgroundImage { get; private set; }

        public void Construct(ImageUI image, ImageUI backgroundImage)
        {
            Image = image ? image : throw new ArgumentNullException(nameof(image));
            BackgroundImage = backgroundImage ? backgroundImage : 
                throw new ArgumentNullException(nameof(backgroundImage));
        }
    }
}