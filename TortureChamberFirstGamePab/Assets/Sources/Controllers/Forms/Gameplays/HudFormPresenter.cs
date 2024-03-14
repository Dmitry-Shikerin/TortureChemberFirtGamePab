using System;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Controllers.Forms.Gameplays
{
    public class HudFormPresenter : PresenterBase
    {
        private readonly IFormService _formService;
        private readonly IHudFormView _hudFormView;

        public HudFormPresenter(IHudFormView hudFormView, IFormService formService)
        {
            _hudFormView = hudFormView ?? throw new ArgumentNullException(nameof(hudFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        public void ShowPauseMenu()
        {
            _formService.Show<PauseMenuFormView>();
        }
    }
}