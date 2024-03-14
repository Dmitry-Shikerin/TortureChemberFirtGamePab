﻿using System;
using Sources.Controllers.Taverns;
using Sources.Domain.Taverns;
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
                tavernMoodView,
                imageUI,
                _upgradeProvider.Charisma);
        }
    }
}