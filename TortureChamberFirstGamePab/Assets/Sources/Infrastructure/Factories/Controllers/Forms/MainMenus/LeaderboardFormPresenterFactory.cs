using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;

namespace Sources.Infrastructure.Factories.Controllers.Forms.MainMenus
{
    public class LeaderboardFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly ILeaderboardInitializeService _leaderboardInitializeService;

        public LeaderboardFormPresenterFactory
        (
            IFormService formService,
            ILeaderboardInitializeService leaderboardInitializeService
        )
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _leaderboardInitializeService = 
                leaderboardInitializeService ??
                throw new ArgumentNullException(nameof(leaderboardInitializeService));
        }

        //TODO в конце убрать галочку DevelopmentBuild
        //TODO плохо работает ротейшн
        public LeaderboardFormPresenter Create(ILeaderboardFormView leaderboardFormView)
        {
            if (leaderboardFormView == null)
                throw new ArgumentNullException(nameof(leaderboardFormView));

            return new LeaderboardFormPresenter(leaderboardFormView,
                _formService, _leaderboardInitializeService);
        }
    }
}