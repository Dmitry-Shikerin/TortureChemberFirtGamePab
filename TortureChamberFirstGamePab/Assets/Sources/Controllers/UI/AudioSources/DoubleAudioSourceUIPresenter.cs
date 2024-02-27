using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class DoubleAudioSourceUIPresenter : PresenterBase
    {
        private readonly IDoubleAudioSourceActivator _audioSourceActivator;
        private readonly IDoubleAudioSourceUI _audioSourceUI;
        private readonly IVolumeService _volumeService;
        private readonly IPauseService _pauseService;

        public DoubleAudioSourceUIPresenter
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IDoubleAudioSourceUI audioSourceUI,
            IVolumeService volumeService,
            IPauseService pauseService
        )
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable()
        {
            OnVolumeChanged(_volumeService.Volume);
            
            _audioSourceActivator.FirstAudioSourceActivated += OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated += OnSecondAudioSourceActivate;

            _volumeService.VolumeChanged += OnVolumeChanged;

            _pauseService.PauseActivated += OnPause;
            _pauseService.ContinueActivated += OnContinue;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;

            _volumeService.VolumeChanged -= OnVolumeChanged;

            _pauseService.PauseActivated -= OnPause;
            _pauseService.ContinueActivated -= OnContinue;
        }

        private void OnContinue()
        {
            _audioSourceUI.FirstAudioSourceView.UnPause();
            _audioSourceUI.SecondAudioSourceView.UnPause();
        }

        private void OnPause()
        {
            _audioSourceUI.FirstAudioSourceView.Pause();
            _audioSourceUI.SecondAudioSourceView.Pause();
        }

        private void OnVolumeChanged(float volume)
        {
            _audioSourceUI.FirstAudioSourceView.SetVolume(volume);
            _audioSourceUI.SecondAudioSourceView.SetVolume(volume);
        }

        private void OnFirstAudioSourceActivate() =>
            _audioSourceUI.FirstAudioSourceView.Play();

        private void OnSecondAudioSourceActivate() =>
            _audioSourceUI.SecondAudioSourceView.Play();
    }
}