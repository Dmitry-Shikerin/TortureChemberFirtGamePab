using System;
using Scripts.Domain.Taverns;
using Scripts.DomainInterfaces.Upgrades;
using Scripts.PresentationInterfaces.UI;

namespace Scripts.Controllers.Taverns
{
    public class TavernMoodPresenter : PresenterBase
    {
        private readonly IImageUI _imageUI;
        private readonly TavernMood _tavernMood;
        private readonly IUpgradable _upgradable;

        public TavernMoodPresenter(
            TavernMood tavernMood,
            IImageUI imageUI,
            IUpgradable upgradable)
        {
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
            _upgradable = upgradable ?? throw new ArgumentNullException(nameof(upgradable));
        }

        public override void Enable()
        {
            OnTavernMoodValueChanged();

            _tavernMood.TavernMoodValueChanged += OnTavernMoodValueChanged;
            _upgradable.CurrentLevelUpgrade.Changed += OnAddedAmountUpgradeChanged;

            _tavernMood.AddedAmountUpgrade = _upgradable.CurrentAmountUpgrade;
        }

        public override void Disable()
        {
            _tavernMood.TavernMoodValueChanged -= OnTavernMoodValueChanged;
            _upgradable.CurrentLevelUpgrade.Changed += OnAddedAmountUpgradeChanged;
        }

        private void OnAddedAmountUpgradeChanged() =>
            _tavernMood.AddedAmountUpgrade = _upgradable.CurrentAmountUpgrade;

        private void OnTavernMoodValueChanged() =>
            _imageUI.SetFillAmount(_tavernMood.TavernMoodValue);
    }
}