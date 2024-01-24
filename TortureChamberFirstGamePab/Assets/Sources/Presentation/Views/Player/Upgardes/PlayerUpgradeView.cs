using Sources.Controllers.Player;
using Sources.PresentationInterfaces.Views.Players;
using TMPro;
using UnityEngine;

namespace Sources.Presentation.Views.Player.Upgardes
{
    public class PlayerUpgradeView : PresentableView<PlayerUpgradePresenter>, IPlayerUpgradeView
    {
        [SerializeField] private TextMeshProUGUI _priceNextUpgrade;
        [SerializeField] private TextMeshProUGUI _currentLevelUpgrade;

        public void SetPriceNextUpgrade(string text)
        {
            _priceNextUpgrade.text = text;
        }

        public void SetCurrentLevelUpgrade(string text)
        {
            _currentLevelUpgrade.text = text;
        }

        public void Upgrade()
        {
            Presenter.Upgrade();
        }
    }
}