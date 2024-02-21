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
        // [SerializeField] private PointerEventData _pointerEventData;
        [SerializeField] private Button _button;

        public void Change()
        {
            // _scrollView.nestedInteractionKind = ScrollView.NestedInteractionKind.ForwardScrolling;
            // _scrollView.nestedInteractionKind = ScrollView.NestedInteractionKind.StopScrolling;
            // PointerEventData pointerEventData = new PointerEventData();
            //
        }
    }
}