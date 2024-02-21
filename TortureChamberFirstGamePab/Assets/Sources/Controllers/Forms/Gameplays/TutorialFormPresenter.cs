using System;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Controllers.Forms.Gameplays
{
    public class TutorialFormPresenter : PresenterBase
    {
        private readonly ITutorialFormView _tutorialFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly Tutorial _tutorial;

        public TutorialFormPresenter
        (
            Setting setting,
            ITutorialFormView tutorialFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _tutorialFormView = tutorialFormView ?? throw new ArgumentNullException(nameof(tutorialFormView));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _tutorial = setting.Tutorial ?? throw new ArgumentNullException(nameof(setting.Tutorial));
        }

        public override void Enable()
        {
            _pauseService.Pause();
            
            _tutorialFormView.DownScrollButton.AddClickListener(DownScroll);
            _tutorialFormView.UpScrollButton.AddClickListener(UpScroll);

            _tutorialFormView.ScrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        public override void Disable()
        {
            _pauseService.Continue();
            
            _tutorialFormView.DownScrollButton.RemoveClickListener(DownScroll);
            _tutorialFormView.UpScrollButton.RemoveClickListener(UpScroll);
            
            HideUpButton();
            
            _tutorialFormView.ScrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            ShowDownButton();
            ShowUpButton();
            HideUpButton();
            HideDownButton();
        }

        public void ShowPauseMenu() => 
            _formService.Show<PauseMenuFormView>();

        private void DownScroll()
        {
            _tutorialFormView.DownScroll(_tutorialFormView.ScrollStep);
            
            HideUpButton();
            ShowUpButton();
            HideDownButton();
        }

        private void UpScroll()
        {
            _tutorialFormView.UpScroll(_tutorialFormView.ScrollStep);
            
            HideDownButton();
            ShowDownButton();
            HideUpButton();
        }

        //TODO порефакторить
        private void HideUpButton()
        {
            if(_tutorialFormView.ScrollRect.verticalNormalizedPosition >= Constant.ScrollRect.MaxValue)
                _tutorialFormView.UpScrollButton.Hide();
        }
        
        private void HideDownButton()
        {
            if (_tutorialFormView.ScrollRect.verticalNormalizedPosition <= 0)
            {
                _tutorialFormView.DownScrollButton.Hide();
                
                _tutorial.HasCompleted = true;
                Debug.Log($"Tutorial HasCompleted {_tutorial.HasCompleted}");
            }
        }
        
        private void ShowUpButton()
        {
            if(_tutorialFormView.ScrollRect.verticalNormalizedPosition < Constant.ScrollRect.MaxValue && 
               _tutorialFormView.UpScrollButton.gameObject.activeSelf == false)
                _tutorialFormView.UpScrollButton.Show();
        }
        
        private void ShowDownButton()
        {
            if(_tutorialFormView.ScrollRect.verticalNormalizedPosition > 0 && 
               _tutorialFormView.DownScrollButton.gameObject.activeSelf == false)
                _tutorialFormView.DownScrollButton.Show();
        }
    }
}