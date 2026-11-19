using System;
using Controllers.Forms.MainMenus;
using InfrastructureInterfaces.Services.Forms;
using PresentationInterfaces.Views.Forms.MainMenus;

namespace Infrastructure.Factories.Controllers.Forms.MainMenus
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