using System.Collections.Generic;
using Presentation.UI;

namespace PresentationInterfaces.Views.Players
{
    public interface IPlayerUpgradeView
    {
        IReadOnlyList<ImageView> ImageViews { get; }

        void SetPriceNextUpgrade(string text);
        void Upgrade();
    }
}