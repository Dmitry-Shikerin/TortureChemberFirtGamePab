using System;
using Sources.Presentation.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Sources.Presentation.UI.ScrollViews
{
    public class ScrollViewUI : View
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;

        //TODO перенести это в туториал форм
        private void Awake()
        {
            _downButton.onClick.AddListener(DownScroll);
            _upButton.onClick.AddListener(UpScroll);
        }

        public void DownScroll()
        {
            _scrollRect.verticalNormalizedPosition -= 0.1f;
        }
        public void UpScroll()
        {
            _scrollRect.verticalNormalizedPosition += 0.1f;
        }
    }
}