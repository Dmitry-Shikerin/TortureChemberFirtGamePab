using System;
using Scripts.Controllers.Forms.Gameplays;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.InfrastructureInterfaces.Services.Forms;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;

namespace Scripts.Infrastructure.Factories.Controllers.Forms.Gameplays
{
    public class TutorialFormPresenterFactory
    {
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly Setting _setting;
        private readonly IDataService<Setting> _settingDataService;

        public TutorialFormPresenterFactory(
            IDataService<Setting> settingDataService,
            Setting setting,
            IFormService formService,
            IPauseService pauseService)
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

            return new TutorialFormPresenter(
                _settingDataService,
                _setting,
                tutorialFormView,
                _formService,
                _pauseService);
        }
    }
}