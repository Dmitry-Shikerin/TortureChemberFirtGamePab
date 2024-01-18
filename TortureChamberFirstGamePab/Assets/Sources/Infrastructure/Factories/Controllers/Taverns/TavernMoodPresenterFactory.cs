using System;
using JetBrains.Annotations;
using Sources.Controllers.Taverns;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Upgrades;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Infrastructure.Factories.Controllers.Taverns
{
    public class TavernMoodPresenterFactory
    {
        private readonly IUpgradeble _upgradeble;

        public TavernMoodPresenterFactory(IUpgradeble upgradeble)
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
        }
        
        public TavernMoodPresenter Create(TavernMood tavernMood, ITavernMoodView tavernMoodView, IImageUI imageUI)
        {
            return new TavernMoodPresenter(tavernMood, tavernMoodView, imageUI, _upgradeble);
        }
    }
}