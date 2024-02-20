using System.Collections.Generic;
using Sources.Presentation.UI;

namespace Sources.PresentationInterfaces.Views.Players
{
    public interface IPlayerUpgradeView
    { 
        IReadOnlyList<ImageView> ImageViews { get; }
        
        void SetPriceNextUpgrade(string text);
        void SetCurrentLevelUpgrade(string text);
        void Upgrade();
    }
}