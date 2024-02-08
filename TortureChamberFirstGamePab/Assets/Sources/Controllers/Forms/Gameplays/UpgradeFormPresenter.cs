using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Controllers.Forms.Gameplays
{
    public class UpgradeFormPresenter : PresenterBase
    {
        private readonly IUpgradeFormView _leaderboardFormView;
        private readonly IFormService _formService;

        public UpgradeFormPresenter(IUpgradeFormView leaderboardFormView, IFormService formService)
        {
            _leaderboardFormView = leaderboardFormView ?? 
                                   throw new ArgumentNullException(nameof(leaderboardFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }
    }
}