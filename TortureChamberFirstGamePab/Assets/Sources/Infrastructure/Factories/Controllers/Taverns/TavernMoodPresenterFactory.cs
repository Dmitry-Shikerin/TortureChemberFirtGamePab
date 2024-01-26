using System;
using JetBrains.Annotations;
using Sources.Controllers.Taverns;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Upgrades;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Infrastructure.Factories.Controllers.Taverns
{
    public class TavernMoodPresenterFactory
    {
        private readonly IUpgradeProvider _upgradeProvider;

        public TavernMoodPresenterFactory(IUpgradeProvider upgradeProvider)
        {
            _upgradeProvider = upgradeProvider ?? throw new ArgumentNullException(nameof(upgradeProvider));
        }
        
        
        
        public TavernMoodPresenter Create(TavernMood tavernMood, ITavernMoodView tavernMoodView, IImageUI imageUI)
        {
            return new TavernMoodPresenter(tavernMood, tavernMoodView, imageUI, _upgradeProvider.Inventory);
        }
    }
}