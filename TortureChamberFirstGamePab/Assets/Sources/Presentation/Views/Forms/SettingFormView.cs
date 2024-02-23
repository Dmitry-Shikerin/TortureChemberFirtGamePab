using System.Collections.Generic;
using Sources.Controllers.Forms;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms;
using UnityEngine;

namespace Sources.Presentation.Views.Forms
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

        public void BackToMainMenu<T>() where T : IFormView => 
            Presenter.BackToMainMenu<T>();
    }
}