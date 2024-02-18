using Sources.Presentation.UI.Texts;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Containers.UI.Texts
{
    public class AdvertisingAfterCertainPeriodTextContainer : View
    {
        [field: SerializeField] public TextView Title { get; private set; }
        [field: SerializeField] public TextView Timer { get; private set; }
    }
}