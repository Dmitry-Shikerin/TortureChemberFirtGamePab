using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class DoubleCallbackAudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IDoubleAudioSourceActivator _audioSourceActivator;
        private readonly IAudioSourceUI _audioSourceUI;
        private readonly IPauseService _pauseService;

        public DoubleCallbackAudioSourceUIPresenter
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI,
            IPauseService pauseService
        )
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable()
        {
            _audioSourceUI.AudioSourceView.SetLoop();
            
            _audioSourceActivator.FirstAudioSourceActivated += OnPlaySound;
            _audioSourceActivator.SecondAudioSourceActivated += OnStopSound;

            _pauseService.PauseActivated += OnPauseSound;
            _pauseService.ContinueActivated += OnContinueSound;
        }

        public override void Disable()
        {
            _audioSourceUI.AudioSourceView.RemoveLoop();
            
            _audioSourceActivator.FirstAudioSourceActivated -= OnPlaySound;
            _audioSourceActivator.SecondAudioSourceActivated -= OnStopSound;
            
            _pauseService.PauseActivated -= OnPauseSound;
            _pauseService.ContinueActivated -= OnContinueSound;
        }

        private void OnPlaySound() =>
            _audioSourceUI.AudioSourceView.Play();

        private void OnStopSound() =>
            _audioSourceUI.AudioSourceView.Stop();

        private void OnPauseSound()
        {
            if(_audioSourceActivator.IsActive == false)
                return;
            
            _audioSourceUI.AudioSourceView.Pause();
        }

        private void OnContinueSound()
        {
            if(_audioSourceActivator.IsActive == false)
                return;
            
            _audioSourceUI.AudioSourceView.Continue();
        }
    }
}