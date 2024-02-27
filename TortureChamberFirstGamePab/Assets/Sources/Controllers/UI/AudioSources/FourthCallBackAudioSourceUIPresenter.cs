using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Sources.Controllers.UI.AudioSources
{
    public class FourthCallBackAudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IFourthAudioSourceActivator _audioSourceActivator;
        private readonly ITripleAudioSourceUI _audioSourceUI;
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public FourthCallBackAudioSourceUIPresenter
        (
            IFourthAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI,
            IPauseService pauseService,
            IVolumeService volumeService
        )
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public override void Enable()
        {
            OnVolumeChanged(_volumeService.Volume);
            
            _audioSourceUI.ThirdAudioSourceView.SetLoop();

            _audioSourceActivator.FirstAudioSourceActivated += OnPlayFirstSound;
            _audioSourceActivator.SecondAudioSourceActivated += OnPlaySecondSound;
            
            _audioSourceActivator.ThirdAudioSourceActivated += OnPlaySound;
            _audioSourceActivator.FourthAudioSourceActivated += OnStopSound;

            _pauseService.PauseActivated += OnPauseSound;
            _pauseService.ContinueActivated += OnContinueSound;

            _volumeService.VolumeChanged += OnVolumeChanged;
        }

        public override void Disable()
        {
            _audioSourceUI.ThirdAudioSourceView.RemoveLoop();
            
            _audioSourceActivator.FirstAudioSourceActivated -= OnPlayFirstSound;
            _audioSourceActivator.SecondAudioSourceActivated -= OnPlaySecondSound;
            
            _audioSourceActivator.ThirdAudioSourceActivated -= OnPlaySound;
            _audioSourceActivator.FourthAudioSourceActivated -= OnStopSound;
            
            _pauseService.PauseActivated -= OnPauseSound;
            _pauseService.ContinueActivated -= OnContinueSound;
            
            _volumeService.VolumeChanged -= OnVolumeChanged;
        }

        private void OnVolumeChanged(float volume)
        {
            _audioSourceUI.FirstAudioSourceView.SetVolume(volume);
            _audioSourceUI.SecondAudioSourceView.SetVolume(volume);
            _audioSourceUI.ThirdAudioSourceView.SetVolume(volume);
        }

        private void OnPlayFirstSound() => 
            _audioSourceUI.FirstAudioSourceView.Play();

        private void OnPlaySecondSound() => 
            _audioSourceUI.SecondAudioSourceView.Play();

        private void OnPlaySound() =>
            _audioSourceUI.ThirdAudioSourceView.Play();

        private void OnStopSound() =>
            _audioSourceUI.ThirdAudioSourceView.Stop();

        private void OnPauseSound()
        {
            Debug.Log($"Audiosource Loop Paused");
            if(_audioSourceActivator.IsActive == false)
                return;
            
            _audioSourceUI.ThirdAudioSourceView.Pause();
            
        }

        private void OnContinueSound()
        {
            Debug.Log($"Audiosource Loop Continue");
            if(_audioSourceActivator.IsActive == false)
                return;
            
            _audioSourceUI.ThirdAudioSourceView.UnPause();
            
        }

    }
}