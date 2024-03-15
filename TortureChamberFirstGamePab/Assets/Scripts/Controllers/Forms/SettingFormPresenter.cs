using System;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.Settings;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.PresentationInterfaces.Views.Forms;
using Scripts.PresentationInterfaces.Views.Forms.Common;
using UnityEngine;

namespace Scripts.Controllers.Forms
{
    public class SettingFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly Setting _setting;
        private readonly IDataService<Setting> _settingDataService;
        private readonly ISettingFormView _view;

        public SettingFormPresenter(
            Setting setting,
            ISettingFormView view,
            IFormService formService,
            IDataService<Setting> settingDataService,
            IPauseService pauseService)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        private Volume Volume => _setting.Volume;

        public override void Enable()
        {
            _pauseService.Pause();

            ShowSprites(Volume.Step);
        }

        public override void Disable()
        {
            _pauseService.Continue();

            _settingDataService.Save(_setting);
        }

        public void IncreaseVolume()
        {
            Volume.Increase();
            ShowSprites(Volume.Step);
        }

        public void TurnDownVolume()
        {
            Volume.TurnDown();
            ShowSprites(Volume.Step);
        }

        public void BackToMainMenu<T>()
            where T : IFormView
        {
            _formService.Show<T>();
        }

        private void ShowSprites(int currentStep)
        {
            SetSpriteInRange(0, currentStep, _view.FilledSprite);
            SetSpriteInRange(currentStep, _view.Images.Count, _view.VoidSprite);
        }

        private void SetSpriteInRange(int from, int to, Sprite sprite)
        {
            for (var i = from; i < to; i++) _view.Images[i].SetSprite(sprite);
        }
    }
}