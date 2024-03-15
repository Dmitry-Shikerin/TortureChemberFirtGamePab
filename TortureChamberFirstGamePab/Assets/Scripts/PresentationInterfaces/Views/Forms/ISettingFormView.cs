using System.Collections.Generic;
using Scripts.Presentation.UI;
using Scripts.PresentationInterfaces.Views.Forms.Common;
using UnityEngine;

namespace Scripts.PresentationInterfaces.Views.Forms
{
    public interface ISettingFormView
    {
        Sprite FilledSprite { get; }
        Sprite VoidSprite { get; }
        List<ImageView> Images { get; }

        void IncreaseVolume();
        void TurnDownVolume();
        void BackToMainMenu<T>()
            where T : IFormView;
    }
}