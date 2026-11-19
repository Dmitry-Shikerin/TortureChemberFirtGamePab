using System.Collections.Generic;
using Controllers.Forms;
using Presentation.UI;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms;
using PresentationInterfaces.Views.Forms.Common;
using UnityEngine;

namespace Presentation.Views.Forms
{
    public class SettingFormView : FormBase<SettingFormPresenter>, ISettingFormView
    {
        [field: SerializeField] public Sprite FilledSprite { get; private set; }
        [field: SerializeField] public Sprite VoidSprite { get; private set; }
        [field: SerializeField] public List<ImageView> Images { get; private set; }

        public void IncreaseVolume() =>
            Presenter.IncreaseVolume();

        public void TurnDownVolume() =>
            Presenter.TurnDownVolume();

        public void BackToMainMenu<T>()
            where T : IFormView =>
            Presenter.BackToMainMenu<T>();
    }
}