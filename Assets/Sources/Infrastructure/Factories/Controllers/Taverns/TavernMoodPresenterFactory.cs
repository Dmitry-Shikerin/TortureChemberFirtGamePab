using System;
using Controllers.Taverns;
using Domain.Taverns;
using InfrastructureInterfaces.Services.Providers.Upgrades;
using PresentationInterfaces.UI;
using PresentationInterfaces.Views.Taverns;

namespace Infrastructure.Factories.Controllers.Taverns
{
    public class TavernMoodPresenterFactory
    {
        private readonly IUpgradeProvider _upgradeProvider;

        public TavernMoodPresenterFactory(IUpgradeProvider upgradeProvider)
        {
            _upgradeProvider = upgradeProvider ??
                               throw new ArgumentNullException(nameof(upgradeProvider));
        }

        public TavernMoodPresenter Create(
            TavernMood tavernMood,
            ITavernMoodView tavernMoodView,
            IImageUI imageUI)
        {
            if (tavernMood == null)
                throw new ArgumentNullException(nameof(tavernMood));

            if (tavernMoodView == null)
                throw new ArgumentNullException(nameof(tavernMoodView));

            if (imageUI == null)
                throw new ArgumentNullException(nameof(imageUI));

            return new TavernMoodPresenter(
                tavernMood,
                imageUI,
                _upgradeProvider.Charisma);
        }
    }
}