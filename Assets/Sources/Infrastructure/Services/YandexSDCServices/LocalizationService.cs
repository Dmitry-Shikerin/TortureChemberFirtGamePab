using System;
using InfrastructureInterfaces.Services.SDCServices;
using Lean.Localization;

namespace Infrastructure.Services.YandexSDCServices
{
    public class LocalizationService : ILocalizationService
    {
        private readonly LeanLocalization _leanLanguage;

        public LocalizationService(LeanLocalization leanLanguage)
        {
            _leanLanguage = leanLanguage
                ? leanLanguage
                : throw new ArgumentNullException(nameof(leanLanguage));
        }

        public void Enter(object payload = null) =>
            LanguageSettings.Apply(_leanLanguage.transform.root.GetComponentsInChildren<LeanLocalization>(true));
    }
}