using System;
using InfrastructureInterfaces.Services.Forms;
using Presentation.Triggers.Taverns;
using Presentation.Views.Forms.Gameplays;
using PresentationInterfaces.Views.Players;

namespace Infrastructure.Services.UpgradeServices
{
    public class TavernUpgradePointService
    {
        private readonly IFormService _formService;

        private readonly TavernUpgradeTrigger _triggerUpgradeTrigger;
        private readonly UpgradeFormView _upgradeFormView;

        public TavernUpgradePointService(TavernUpgradeTrigger tavernUpgradeTrigger, IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _triggerUpgradeTrigger = tavernUpgradeTrigger
                ? tavernUpgradeTrigger
                : throw new ArgumentNullException(nameof(tavernUpgradeTrigger));
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
            _formService.Show<UpgradeFormView>();

        private void OnExit(IPlayerMovementView playerMovementView) =>
            _formService.Show<HudFormView>();
    }
}