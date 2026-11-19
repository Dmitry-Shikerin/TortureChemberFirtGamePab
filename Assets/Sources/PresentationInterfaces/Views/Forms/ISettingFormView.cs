using System.Collections.Generic;
using Presentation.UI;
using PresentationInterfaces.Views.Forms.Common;
using UnityEngine;

namespace PresentationInterfaces.Views.Forms
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