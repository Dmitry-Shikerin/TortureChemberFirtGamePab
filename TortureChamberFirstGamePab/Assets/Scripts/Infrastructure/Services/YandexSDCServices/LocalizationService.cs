using System;
using Agava.WebUtility;
using Agava.YandexGames;
using Lean.Localization;
using Scripts.Domain.Constants;
using Scripts.InfrastructureInterfaces.Services.SDCServices;

namespace Scripts.Infrastructure.Services.YandexSDCServices
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
            ChangeLanguage();

        private void ChangeLanguage()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            var languageCode = YandexGamesSdk.Environment.i18n.lang switch
            {
                LocalizationConstant.English => LocalizationConstant.EnglishCode,
                LocalizationConstant.Turkish => LocalizationConstant.TurkishCode,
                LocalizationConstant.Russian => LocalizationConstant.RussianCode,
                _ => Anonymous.English
            };

            _leanLanguage.SetCurrentLanguage(languageCode);
        }
    }
}