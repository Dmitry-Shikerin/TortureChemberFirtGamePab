using System.Collections.Generic;
using Controllers.Player;
using Presentation.UI;
using PresentationInterfaces.Views.Players;
using TMPro;
using UnityEngine;

namespace Presentation.Views.Player.Upgardes
{
    public class PlayerUpgradeView : PresentableView<PlayerUpgradePresenter>, IPlayerUpgradeView
    {
        [SerializeField] private List<ImageView> _imageViews;
        [SerializeField] private TextMeshProUGUI _priceNextUpgrade;
        [SerializeField] private TextMeshProUGUI _currentLevelUpgrade;

        public IReadOnlyList<ImageView> ImageViews => _imageViews;

        public void SetPriceNextUpgrade(string text) =>
            _priceNextUpgrade.text = text;

        public void Upgrade() =>
            Presenter.Upgrade();
    }
}