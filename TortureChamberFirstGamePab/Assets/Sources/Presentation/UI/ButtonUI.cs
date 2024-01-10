using System;
using Sources.Controllers.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    public class ButtonUI : PresentableView<ButtonUIPresenter>, IButtonUI
    {
        [SerializeField] private Button _button;
        
        //TODO включать обьект когда сосдался презентер
        protected override void OnAfterEnable()
        {
            _button.onClick.AddListener(Presenter.OnClick);
        }

        protected override void OnAfterDisable()
        {
            _button.onClick.RemoveListener(Presenter.OnClick);
        }

        // public void Start()
        // {
        //     _button.onClick.AddListener(Presenter.AddListener);
        // }
    }
}