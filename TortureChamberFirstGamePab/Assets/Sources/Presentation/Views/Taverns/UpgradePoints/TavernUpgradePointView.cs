using System;
using Sources.Controllers.Taverns;
using Sources.PresentationInterfaces.Views.Taverns.TavernUpgradePoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointView : PresentableView<TavernUpgradePointPresenter>, ITavernUpgradePoint
    {
        private void Start()
        {
            Hide();
        }
    }
}