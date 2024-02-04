using System;
using Agava.YandexGames;
using Lean.Localization;
using Sources.Domain.Constants;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class LocalizationService
    {
        private readonly LeanLocalization _leanLanguage;

        //TODO Доделать локализацию
        public LocalizationService(LeanLocalization leanLanguage)
        {
            _leanLanguage = leanLanguage 
                ? leanLanguage 
                : throw new ArgumentNullException(nameof(leanLanguage));
        }

        public void Enter()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang switch
            {
                Constant.Localization.English => Constant.Localization.EnglishCode,
                Constant.Localization.Turkish => Constant.Localization.TurkishCode,
                Constant.Localization.Russian => Constant.Localization.RussianCode,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _leanLanguage.SetCurrentLanguage(languageCode);
        }
    }
}