using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class TutorialFormPresenterFactory
    {
        private readonly IDataService<Setting> _settingDataService;
        private readonly Setting _setting;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;

        public TutorialFormPresenterFactory
        (
            IDataService<Setting> settingDataService,
            Setting setting,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public TutorialFormPresenter Create(ITutorialFormView tutorialFormView)
        {
            if (tutorialFormView == null) 
                throw new ArgumentNullException(nameof(tutorialFormView));
            
            return new TutorialFormPresenter(_settingDataService,_setting,
                tutorialFormView, _formService, _pauseService);
        }
    }
}