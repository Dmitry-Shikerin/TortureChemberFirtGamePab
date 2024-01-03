using System;
using Sources.Controllers.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    public class ButtonUI : PresentableView<ButtonUIPresenter>, IButtonUI
    {
        [SerializeField] private Button _button;
        
        //TODO вылетают эксепшены
        private void Start()
        {
            _button.onClick.AddListener(Presenter.AddListener);
        }
    }
}