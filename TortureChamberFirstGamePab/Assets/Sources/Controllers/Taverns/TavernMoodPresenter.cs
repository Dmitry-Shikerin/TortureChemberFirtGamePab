using System;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Upgrades;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Controllers.Taverns
{
    public class TavernMoodPresenter : PresenterBase
    {
        private readonly TavernMood _tavernMood;
        private readonly ITavernMoodView _tavernMoodView;
        private readonly IImageUI _imageUI;
        private readonly IUpgradeble _upgradeble;

        public TavernMoodPresenter
        (
            TavernMood tavernMood,
            ITavernMoodView tavernMoodView,
            IImageUI imageUI,
            IUpgradeble upgradeble
        )
        {
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _tavernMoodView = tavernMoodView ?? throw new ArgumentNullException(nameof(tavernMoodView));
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));

        }

        public override void Enable()
        {
            _imageUI.SetFillAmount(Constant.TavernMoodValues.StartValue);
            
            _tavernMood.TavernMoodValueChanged += OnTavernMoodValueChanged;
            _upgradeble.CurrentLevelUpgrade.Changed += OnAddedAmountUpgradeChanged;
            
            _tavernMood.AddedAmountUpgrade = _upgradeble.AddedAmountUpgrade;
        }

        public override void Disable()
        {
            _tavernMood.TavernMoodValueChanged -= OnTavernMoodValueChanged;
            _upgradeble.CurrentLevelUpgrade.Changed += OnAddedAmountUpgradeChanged;
        }

        private void OnAddedAmountUpgradeChanged() => 
            _tavernMood.AddedAmountUpgrade = _upgradeble.AddedAmountUpgrade;

        private void OnTavernMoodValueChanged() => 
            _imageUI.SetFillAmount(_tavernMood.TavernMoodValue);
    }
}