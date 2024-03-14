using System;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Upgrades;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Controllers.Taverns
{
    public class TavernMoodPresenter : PresenterBase
    {
        private readonly IImageUI _imageUI;
        private readonly TavernMood _tavernMood;
        private readonly ITavernMoodView _tavernMoodView;
        private readonly IUpgradeble _upgradeble;

        public TavernMoodPresenter(
            TavernMood tavernMood,
            ITavernMoodView tavernMoodView,
            IImageUI imageUI,
            IUpgradeble upgradeble)
        {
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _tavernMoodView = tavernMoodView ?? throw new ArgumentNullException(nameof(tavernMoodView));
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
        }

        public override void Enable()
        {
            OnTavernMoodValueChanged();

            _tavernMood.TavernMoodValueChanged += OnTavernMoodValueChanged;
            _upgradeble.CurrentLevelUpgrade.Changed += OnAddedAmountUpgradeChanged;

            _tavernMood.AddedAmountUpgrade = _upgradeble.CurrentAmountUpgrade;
        }

        public override void Disable()
        {
            _tavernMood.TavernMoodValueChanged -= OnTavernMoodValueChanged;
            _upgradeble.CurrentLevelUpgrade.Changed += OnAddedAmountUpgradeChanged;
        }

        private void OnAddedAmountUpgradeChanged()
        {
            _tavernMood.AddedAmountUpgrade = _upgradeble.CurrentAmountUpgrade;
        }

        private void OnTavernMoodValueChanged()
        {
            _imageUI.SetFillAmount(_tavernMood.TavernMoodValue);
        }
    }
}