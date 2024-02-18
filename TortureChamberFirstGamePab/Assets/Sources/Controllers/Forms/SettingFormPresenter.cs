using System;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
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

        public SettingFormPresenter
        (
            Setting setting,
            ISettingFormView view,
            IFormService formService,
            IDataService<Setting> settingDataService
        )
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
        }

        private Volume Volume => _setting.Volume;

        public override void Enable()
        {
            ShowSprites(Volume.Step);
        }

        public override void Disable()
        {
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

        public void BackToMainMenu() =>
            _formService.Show<MainMenuFormView>();

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