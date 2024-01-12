using System;
using MyProject.Sources.PresentationInterfaces.Views;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointService
    {
        private readonly TavernUpgradeTrigger _triggerUpgradeTrigger;
        private readonly TavernUpgradePointView _tavernUpgradePointView;

        public TavernUpgradePointService(TavernUpgradeTrigger tavernUpgradeTrigger,
            TavernUpgradePointView tavernUpgradePointView)
        {
            _triggerUpgradeTrigger = tavernUpgradeTrigger
                ? tavernUpgradeTrigger
                : throw new ArgumentNullException(nameof(tavernUpgradeTrigger));
            _tavernUpgradePointView = tavernUpgradePointView
                ? tavernUpgradePointView
                : throw new ArgumentNullException(nameof(tavernUpgradePointView));
        }

        public void OnEnable()
        {
            _triggerUpgradeTrigger.Entered += OnEnter;
            _triggerUpgradeTrigger.Exited += OnExit;
        }

        public void OnDisable()
        {
            _triggerUpgradeTrigger.Entered -= OnEnter;
            _triggerUpgradeTrigger.Exited -= OnExit;
        }

        private void OnEnter(IPlayerMovementView playerMovementView) => 
            _tavernUpgradePointView.Show();

        private void OnExit(IPlayerMovementView playerMovementView) =>
            _tavernUpgradePointView.Hide();
    }
}