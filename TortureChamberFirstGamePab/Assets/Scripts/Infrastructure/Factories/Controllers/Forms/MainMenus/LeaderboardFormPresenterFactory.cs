using System;
using Scripts.Controllers.Forms.MainMenus;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;

namespace Scripts.Infrastructure.Factories.Controllers.Forms.MainMenus
{
    public class LeaderboardFormPresenterFactory
    {
        private readonly IFormService _formService;

        public LeaderboardFormPresenterFactory(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public LeaderboardFormPresenter Create(ILeaderboardFormView leaderboardFormView)
        {
            if (leaderboardFormView == null)
                throw new ArgumentNullException(nameof(leaderboardFormView));

            return new LeaderboardFormPresenter(_formService);
        }
    }
}