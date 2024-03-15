using System;
using Scripts.Controllers.Taverns;
using Scripts.Domain.Taverns;
using Scripts.InfrastructureInterfaces.Services.Providers.Upgrades;
using Scripts.PresentationInterfaces.UI;
using Scripts.PresentationInterfaces.Views.Taverns;

namespace Scripts.Infrastructure.Factories.Controllers.Taverns
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