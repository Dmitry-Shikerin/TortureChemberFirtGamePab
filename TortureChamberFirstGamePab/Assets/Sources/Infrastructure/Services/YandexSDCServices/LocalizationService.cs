using System;
using Agava.YandexGames;
using Lean.Localization;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IWebGlService _webGlService;
        private readonly LeanLocalization _leanLanguage;

        public LocalizationService(LeanLocalization leanLanguage, IWebGlService webGlService)
        {
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
            _leanLanguage = leanLanguage 
                ? leanLanguage 
                : throw new ArgumentNullException(nameof(leanLanguage));
        }

        public void Enter(object payload = null)
        {
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            if(_webGlService.IsWebGl == false)
                return;
            
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