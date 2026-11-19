using Presentation.UI.Texts;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.Texts
{
    public class AdvertisingAfterCertainPeriodViewContainer : View
    {
        [field: SerializeField] public TextView Title { get; private set; }
        [field: SerializeField] public TextView Timer { get; private set; }
    }
}