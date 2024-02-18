using System.Collections.Generic;
using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Forms
{
    public interface ISettingFormView
    {
        Sprite FilledSprite { get; }
        Sprite VoidSprite { get; }
        List<ImageView> Images { get; }

        
        void IncreaseVolume();
        void TurnDownVolume();
        void BackToMainMenu();
    }
}