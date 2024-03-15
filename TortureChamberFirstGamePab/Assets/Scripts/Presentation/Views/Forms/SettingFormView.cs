using System.Collections.Generic;
using Scripts.Controllers.Forms;
using Scripts.Presentation.UI;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms;
using Scripts.PresentationInterfaces.Views.Forms.Common;
using UnityEngine;

namespace Scripts.Presentation.Views.Forms
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