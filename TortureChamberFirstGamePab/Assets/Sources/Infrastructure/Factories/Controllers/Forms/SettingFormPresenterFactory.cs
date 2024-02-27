using System;
using Sources.Controllers.Forms;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Forms;

namespace Sources.Infrastructure.Factories.Controllers.Forms
{
    public class SettingFormPresenterFactory
    {
        private readonly Setting _setting;
        private readonly IFormService _formService;
        private readonly IDataService<Setting> _settingDataService;
        private readonly IPauseService _pauseService;

        public SettingFormPresenterFactory
        (
            Setting setting,
            IFormService formService,
            IDataService<Setting> settingDataService,
            IPauseService pauseService
        )
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
            
            return new SettingFormPresenter(_setting, formView, 
                _formService, _settingDataService, _pauseService);
        }
    }
}