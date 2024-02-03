using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

namespace Sources.Presentation.Views.YandexSDC
{
    public class Localization : MonoBehaviour
    {
        //TODO вынести константы
        private const string EnglishCode = "English";
        private const string RussianCode = "Russian";
        private const string TurkishCode = "Turkish";
        private const string Turkish = "tr";
        private const string Russian = "ru";
        private const string English = "en";

        //TODO что это?
        //TODO как пользоваться этим плагином?
        [SerializeField] private LeanLocalization _leanLanguage;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            //TODO сделать по человечески
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLanguage.SetCurrentLanguage(EnglishCode);
                    break;
                
                case Turkish:
                    _leanLanguage.SetCurrentLanguage(TurkishCode);
                    break;
                
                case Russian:
                    _leanLanguage.SetCurrentLanguage(RussianCode);
                    break;
            }

            
            //TODO помоему такая запись лучше
            // string language = YandexGamesSdk.Environment.i18n.lang switch
            // {
            //     English => EnglishCode,
            //     Turkish => TurkishCode,
            //     Russian => RussianCode
            // };
            //
            // _leanLanguage.SetCurrentLanguage(language);
        }
    }
}