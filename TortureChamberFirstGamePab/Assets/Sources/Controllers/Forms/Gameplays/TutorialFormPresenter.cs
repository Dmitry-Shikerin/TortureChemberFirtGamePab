﻿using System;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
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
        private readonly IDataService<Setting> _settingDataService;
        private readonly Setting _setting;
        private readonly ITutorialFormView _tutorialFormView;
        private readonly IFormService _formService;
        private readonly IPauseService _pauseService;
        private readonly Tutorial _tutorial;

        public TutorialFormPresenter
        (
            IDataService<Setting> settingDataService,
            Setting setting,
            ITutorialFormView tutorialFormView,
            IFormService formService,
            IPauseService pauseService
        )
        {
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
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
            
            _settingDataService.Save(_setting);
        }

        //TODO временное решение
        public void ClearCompleteTutorial()
        {
            _setting.Tutorial.HasCompleted = false;
            
            _settingDataService.Save(_setting);
            
            Debug.Log("Tutorial очищен");
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

        //TODO поправить скрывание кнопок
        private void UpScroll()
        {
            _tutorialFormView.UpScroll(_tutorialFormView.ScrollStep);
            
            HideDownButton();
            ShowDownButton();
            HideUpButton();
        }

        //TODO добавить туториал для сложности
        //TODO добавить туториал для рекламы
        //TODO добавить туториал для управления
        //TODO добавить туториал для сохранений
        //TODO порефакторить
        private void HideUpButton()
        {
            if(_tutorialFormView.ScrollRect.verticalNormalizedPosition >= Constant.ScrollRect.MaxValue)
                _tutorialFormView.UpScrollButton.Hide();
        }
        
        private void HideDownButton()
        {
            if (_tutorialFormView.ScrollRect.verticalNormalizedPosition <= Constant.ScrollRect.MinValue)
            {
                _tutorialFormView.DownScrollButton.Hide();
                
                if(_tutorial.HasCompleted)
                    return;
                    
                _tutorial.HasCompleted = true;
                
                _settingDataService.Save(_setting);
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
            if(_tutorialFormView.ScrollRect.verticalNormalizedPosition > Constant.ScrollRect.MinValue && 
               _tutorialFormView.DownScrollButton.gameObject.activeSelf == false)
                _tutorialFormView.DownScrollButton.Show();
        }
    }
}