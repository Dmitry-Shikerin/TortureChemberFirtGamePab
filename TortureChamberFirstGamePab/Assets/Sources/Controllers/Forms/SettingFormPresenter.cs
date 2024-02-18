using System;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.Views.Forms;

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

        public void BackToMainMenu<T>() where T : IFormView
        {
            _formService.Show<T>();
        }

        private void ShowSprites(int currentStep)
        {
            for (int i = 0; i < _view.Images.Count; i++)
            {
                if (i < currentStep)
                {
                    _view.Images[i].SetSprite(_view.FilledSprite);

                    continue;
                }

                _view.Images[i].SetSprite(_view.VoidSprite);
            }
        }
    }
}