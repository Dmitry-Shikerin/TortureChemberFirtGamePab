using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Infrastructure.Factories.Controllers.Forms.MainMenus
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

            return new LeaderboardFormPresenter(leaderboardFormView, _formService);
        }
    }
}