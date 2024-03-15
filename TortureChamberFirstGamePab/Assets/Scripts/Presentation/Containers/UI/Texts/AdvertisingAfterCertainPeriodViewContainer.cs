using Scripts.Presentation.UI.Texts;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Texts
{
    public class AdvertisingAfterCertainPeriodViewContainer : View
    {
        [field: SerializeField] public TextView Title { get; private set; }
        [field: SerializeField] public TextView Timer { get; private set; }
    }
}