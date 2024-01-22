using Sources.Controllers.UI;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    public class ButtonUI : PresentableView<ButtonUIPresenter>, IButtonUI
    {
        [SerializeField] private Button _button;
        
        protected override void OnAfterEnable() => 
            _button.onClick.AddListener(Presenter.OnClick);

        protected override void OnAfterDisable() => 
            _button.onClick.RemoveListener(Presenter.OnClick);

        public void Enable()
        {
            _button.enabled = true;
        }

        public void Disable()
        {
            _button.enabled = false;
        }
    }
}