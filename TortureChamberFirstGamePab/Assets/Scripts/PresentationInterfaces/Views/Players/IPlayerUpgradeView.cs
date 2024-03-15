using System.Collections.Generic;
using Scripts.Presentation.UI;

namespace Scripts.PresentationInterfaces.Views.Players
{
    public interface IPlayerUpgradeView
    {
        IReadOnlyList<ImageView> ImageViews { get; }

        void SetPriceNextUpgrade(string text);
        void Upgrade();
    }
}