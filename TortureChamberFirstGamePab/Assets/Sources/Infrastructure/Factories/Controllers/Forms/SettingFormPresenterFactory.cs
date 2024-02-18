using System;
using Sources.Controllers.Forms;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.PresentationInterfaces.Views.Forms;

namespace Sources.Infrastructure.Factories.Controllers.Forms
{
    public class SettingFormPresenterFactory
    {
        private readonly Setting _setting;
        private readonly IFormService _formService;
        private readonly IDataService<Setting> _settingDataService;

        public SettingFormPresenterFactory
        (
            Setting setting,
            IFormService formService,
            IDataService<Setting> settingDataService
        )
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
        }

        public SettingFormPresenter Create(ISettingFormView formView)
        {
            return new SettingFormPresenter(_setting, formView, _formService, _settingDataService);
        }
    }
}