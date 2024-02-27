using System;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Forms;
using UnityEngine;

namespace Sources.Controllers.Forms
{
    public class SettingFormPresenter : PresenterBase
    {
        private readonly Setting _setting;
        private readonly ISettingFormView _view;
        private readonly IFormService _formService;
        private readonly IDataService<Setting> _settingDataService;
        private readonly IPauseService _pauseService;

        public SettingFormPresenter
        (
            Setting setting,
            ISettingFormView view,
            IFormService formService,
            IDataService<Setting> settingDataService,
            IPauseService pauseService
        )
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

            //TODo покашто закомментировал
            _settingDataService.Save(_setting);
        }

        public void IncreaseVolume()
        {
            Debug.Log($"Increase Volume {Volume.Step}");
            Volume.Increase();
            ShowSprites(Volume.Step);
        }

        public void TurnDownVolume()
        {
            Debug.Log($"Turn Down Volume {Volume.Step}");

            Volume.TurnDown();
            ShowSprites(Volume.Step);
        }

        public void BackToMainMenu<T>() where T : IFormView => 
            _formService.Show<T>();

        private void ShowSprites(int currentStep)
        {
            SetSpriteInRange(0, currentStep, _view.FilledSprite);
            SetSpriteInRange(currentStep, _view.Images.Count, _view.VoidSprite);
        }

        private void SetSpriteInRange(int from, int to, Sprite sprite)
        {
            for (int i = from; i < to; i++)
            {
                _view.Images[i].SetSprite(sprite);
            }
        }
    }
}