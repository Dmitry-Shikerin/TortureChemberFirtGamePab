using Sources.Presentation.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI.ScrollViews
{
    public class ScrollRectView : View
    {
        [SerializeField] private ScrollRect _scrollRect;

        public float VerticalNormalizedPosition => _scrollRect.verticalNormalizedPosition;
        public ScrollRect ScrollRect => _scrollRect;

        public void DownScroll(float step)
        {
            _scrollRect.verticalNormalizedPosition -= step;
        }

        public void UpScroll(float step)
        {
            _scrollRect.verticalNormalizedPosition += step;
        }
    }
}