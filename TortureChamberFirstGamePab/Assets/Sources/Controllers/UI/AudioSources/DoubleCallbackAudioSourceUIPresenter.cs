using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class DoubleCallbackAudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IDoubleAudioSourceActivator _audioSourceActivator;
        private readonly IAudioSourceUI _audioSourceUI;
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public DoubleCallbackAudioSourceUIPresenter(
            IDoubleAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI,
            IPauseService pauseService,
            IVolumeService volumeService)
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public override void Enable()
        {
            OnVolumeChanged();

            _audioSourceUI.AudioSourceView.SetLoop();

            _audioSourceActivator.FirstAudioSourceActivated += OnPlaySound;
            _audioSourceActivator.SecondAudioSourceActivated += OnStopSound;

            _pauseService.PauseActivated += OnPauseSound;
            _pauseService.ContinueActivated += OnContinueSound;

            _volumeService.VolumeChanged += OnVolumeChanged;
        }

        public override void Disable()
        {
            _audioSourceUI.AudioSourceView.RemoveLoop();

            _audioSourceActivator.FirstAudioSourceActivated -= OnPlaySound;
            _audioSourceActivator.SecondAudioSourceActivated -= OnStopSound;

            _pauseService.PauseActivated -= OnPauseSound;
            _pauseService.ContinueActivated -= OnContinueSound;

            _volumeService.VolumeChanged -= OnVolumeChanged;
        }

        private void OnVolumeChanged()
        {
            _audioSourceUI.AudioSourceView.SetVolume(_volumeService.Volume);
        }

        private void OnPlaySound()
        {
            _audioSourceUI.AudioSourceView.Play();
        }

        private void OnStopSound()
        {
            _audioSourceUI.AudioSourceView.Stop();
        }

        private void OnPauseSound()
        {
            if (_audioSourceActivator.IsActive == false)
                return;

            _audioSourceUI.AudioSourceView.Pause();
        }

        private void OnContinueSound()
        {
            if (_audioSourceActivator.IsActive == false)
                return;

            _audioSourceUI.AudioSourceView.Continue();
        }
    }
}