using System;
using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.Settings;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.Presentation.Views.Forms.Gameplays;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;
using UnityEngine;

namespace Scripts.Controllers.Forms.Gameplays
{
    public class TutorialFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly Setting _setting;
        private readonly IDataService<Setting> _settingDataService;
        private readonly Tutorial _tutorial;
        private readonly ITutorialFormView _tutorialFormView;

        public TutorialFormPresenter(
            IDataService<Setting> settingDataService,
            Setting setting,
            ITutorialFormView tutorialFormView,
            IFormService formService,
            IPauseService pauseService)
        {
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _tutorialFormView = tutorialFormView ?? throw new ArgumentNullException(nameof(tutorialFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _tutorial = setting.Tutorial ?? throw new ArgumentNullException(nameof(setting.Tutorial));
        }

        public override void Enable()
        {
            _pauseService.Pause();

            _tutorialFormView.DownScrollButton.AddClickListener(DownScroll);
            _tutorialFormView.UpScrollButton.AddClickListener(UpScroll);

            _tutorialFormView.ScrollRect.ScrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        public override void Disable()
        {
            _pauseService.Continue();

            _tutorialFormView.DownScrollButton.RemoveClickListener(DownScroll);
            _tutorialFormView.UpScrollButton.RemoveClickListener(UpScroll);

            HideUpButton();

            _tutorialFormView.ScrollRect.ScrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);

            _settingDataService.Save(_setting);
        }

        public void ShowPauseMenu() =>
            _formService.Show<PauseMenuFormView>();

        private void OnScrollValueChanged(Vector2 value)
        {
            ShowDownButton();
            ShowUpButton();
            HideUpButton();
            HideDownButton();
        }

        private void DownScroll()
        {
            _tutorialFormView.ScrollRect.DownScroll(_tutorialFormView.ScrollStep);

            HideUpButton();
            ShowUpButton();
            HideDownButton();
        }

        private void UpScroll()
        {
            _tutorialFormView.ScrollRect.UpScroll(_tutorialFormView.ScrollStep);

            HideDownButton();
            ShowDownButton();
            HideUpButton();
        }

        private void HideUpButton()
        {
            if (_tutorialFormView.ScrollRect.VerticalNormalizedPosition >= ScrollRectConstant.MaxValue)
                _tutorialFormView.UpScrollButton.Hide();
        }

        private void HideDownButton()
        {
            if (_tutorialFormView.ScrollRect.VerticalNormalizedPosition <= ScrollRectConstant.MinValue)
            {
                _tutorialFormView.DownScrollButton.Hide();

                if (_tutorial.HasCompleted)
                    return;

                _tutorial.HasCompleted = true;

                _settingDataService.Save(_setting);
            }
        }

        private void ShowUpButton()
        {
            if (_tutorialFormView.ScrollRect.VerticalNormalizedPosition < ScrollRectConstant.MaxValue &&
                _tutorialFormView.UpScrollButton.gameObject.activeSelf == false)
                _tutorialFormView.UpScrollButton.Show();
        }

        private void ShowDownButton()
        {
            if (_tutorialFormView.ScrollRect.VerticalNormalizedPosition > ScrollRectConstant.MinValue &&
                _tutorialFormView.DownScrollButton.gameObject.activeSelf == false)
                _tutorialFormView.DownScrollButton.Show();
        }
    }
}