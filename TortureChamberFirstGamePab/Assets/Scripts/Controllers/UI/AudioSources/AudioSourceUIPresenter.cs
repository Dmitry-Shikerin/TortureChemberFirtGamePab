using System;
using Scripts.DomainInterfaces.UI.AudioSourcesActivators;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.InfrastructureInterfaces.Services.VolumeServices;
using Scripts.PresentationInterfaces.UI.AudioSources;

namespace Scripts.Controllers.UI.AudioSources
{
    public class AudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IAudioSourceActivator _audioSourceActivator;
        private readonly IAudioSourceUI _audioSourceUI;
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public AudioSourceUIPresenter(
            IAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI,
            IVolumeService volumeService,
            IPauseService pauseService)
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable()
        {
            OnVolumeChanged();

            _audioSourceActivator.AudioSourceActivated += OnAudioSourcePlay;

            _volumeService.VolumeChanged += OnVolumeChanged;

            _pauseService.PauseActivated += OnPause;
            _pauseService.ContinueActivated += OnContinue;

            _pauseService.PauseSoundActivated += OnMute;
            _pauseService.ContinueSoundActivated += OnUnMute;
        }

        public override void Disable()
        {
            _audioSourceActivator.AudioSourceActivated -= OnAudioSourcePlay;

            _volumeService.VolumeChanged -= OnVolumeChanged;

            _pauseService.PauseActivated -= OnPause;
            _pauseService.ContinueActivated -= OnContinue;

            _pauseService.PauseSoundActivated -= OnMute;
            _pauseService.ContinueSoundActivated -= OnUnMute;
        }

        private void OnMute()
        {
            _audioSourceUI.AudioSourceView.Mute();
        }

        private void OnUnMute()
        {
            _audioSourceUI.AudioSourceView.UnMute();
        }

        private void OnContinue()
        {
            _audioSourceUI.AudioSourceView.Continue();
        }

        private void OnPause()
        {
            _audioSourceUI.AudioSourceView.Pause();
        }

        private void OnVolumeChanged()
        {
            _audioSourceUI.AudioSourceView.SetVolume(_volumeService.Volume);
        }

        private void OnAudioSourcePlay()
        {
            _audioSourceUI.AudioSourceView.Play();
        }
    }
}