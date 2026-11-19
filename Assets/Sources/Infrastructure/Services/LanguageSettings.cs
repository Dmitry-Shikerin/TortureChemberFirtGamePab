using Domain.Constants;
using Lean.Localization;
using UnityEngine;

namespace Infrastructure.Services
{
    public static class LanguageSettings
    {
        private const string LanguageKey = "Settings.Language";

        public static string CurrentLanguage
        {
            get
            {
                string language = PlayerPrefs.GetString(LanguageKey, LocalizationConstant.EnglishCode);
                return language == LocalizationConstant.RussianCode
                    ? LocalizationConstant.RussianCode
                    : LocalizationConstant.EnglishCode;
            }
        }

        public static void SelectLanguage(string language)
        {
            string selectedLanguage = language == LocalizationConstant.RussianCode
                ? LocalizationConstant.RussianCode
                : LocalizationConstant.EnglishCode;
            PlayerPrefs.SetString(LanguageKey, selectedLanguage);
            PlayerPrefs.Save();
        }

        public static void Apply(LeanLocalization[] localizations)
        {
            string language = CurrentLanguage;

            foreach (LeanLocalization localization in localizations)
            {
                localization.DetectLanguage = LeanLocalization.DetectType.None;
                localization.SaveLoad = LeanLocalization.SaveLoadType.None;
                localization.DefaultLanguage = LocalizationConstant.EnglishCode;
                localization.SetCurrentLanguage(language);
            }
        }
    }
}