using System;
using Scripts.Controllers.Forms;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.PresentationInterfaces.Views.Forms;

namespace Scripts.Infrastructure.Factories.Controllers.Forms
{
    public class SettingFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly Setting _setting;
        private readonly IDataService<Setting> _settingDataService;

        public SettingFormPresenterFactory(
            Setting setting,
            IFormService formService,
            IDataService<Setting> settingDataService,
            IPauseService pauseService)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public SettingFormPresenter Create(ISettingFormView formView)
        {
            if (formView == null)
                throw new ArgumentNullException(nameof(formView));

            return new SettingFormPresenter(_setting, formView, _formService, _settingDataService, _pauseService);
        }
    }
}