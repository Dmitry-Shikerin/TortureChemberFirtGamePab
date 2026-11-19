using Domain.Constants;
using Infrastructure.Services;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Views.Forms
{
    public class LanguageSettingsView : MonoBehaviour
    {
        [SerializeField] private LeanLocalization[] _localizations;
        [SerializeField] private Button _previousLanguageButton;
        [SerializeField] private Button _nextLanguageButton;
        [SerializeField] private TMP_Text _languageLabel;
        [SerializeField] private TMP_Text _selectedLanguageLabel;

        private void Awake()
        {
            LanguageSettings.Apply(_localizations);
        }

        private void OnEnable()
        {
            _previousLanguageButton.onClick.AddListener(ChangeLanguage);
            _nextLanguageButton.onClick.AddListener(ChangeLanguage);
            RefreshLabels();
        }

        private void OnDisable()
        {
            _previousLanguageButton.onClick.RemoveListener(ChangeLanguage);
            _nextLanguageButton.onClick.RemoveListener(ChangeLanguage);
        }

        public void ChangeLanguage()
        {
            string language = LanguageSettings.CurrentLanguage == LocalizationConstant.EnglishCode
                ? LocalizationConstant.RussianCode
                : LocalizationConstant.EnglishCode;
            LanguageSettings.SelectLanguage(language);
            LanguageSettings.Apply(_localizations);
            RefreshLabels();
        }

        private void RefreshLabels()
        {
            bool isRussian = LanguageSettings.CurrentLanguage == LocalizationConstant.RussianCode;
            _languageLabel.text = isRussian ? "Язык" : "Language";
            _selectedLanguageLabel.text = isRussian ? "Русский" : "English";
        }
    }
}