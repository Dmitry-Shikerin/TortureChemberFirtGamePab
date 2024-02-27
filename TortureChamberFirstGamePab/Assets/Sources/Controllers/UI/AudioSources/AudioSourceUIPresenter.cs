using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class AudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IAudioSourceActivator _audioSourceActivator;
        private readonly IAudioSourceUI _audioSourceUI;
        private readonly IVolumeService _volumeService;
        private readonly IPauseService _pauseService;

        public AudioSourceUIPresenter
        (
            IAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI,
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

        //TODO сразы выставлять громкость аудио соурсов в подписке
        public override void Enable()
        {
            OnVolumeChanged(_volumeService.Volume);
            
            _audioSourceActivator.AudioSourceActivated += OnAudioSourcePlay;

            _volumeService.VolumeChanged += OnVolumeChanged;

            _pauseService.PauseActivated += OnPause;
            _pauseService.ContinueActivated += OnContinue;
        }

        public override void Disable()
        {
            _audioSourceActivator.AudioSourceActivated -= OnAudioSourcePlay;
            
            _volumeService.VolumeChanged -= OnVolumeChanged;
            
            _pauseService.PauseActivated -= OnPause;
            _pauseService.ContinueActivated -= OnContinue;
        }

        private void OnPause()
        {
            _audioSourceUI.AudioSourceView.Pause();
        }

        private void OnContinue()
        {
            _audioSourceUI.AudioSourceView.UnPause();
        }

        private void OnVolumeChanged(float volume) => 
            _audioSourceUI.AudioSourceView.SetVolume(volume);

        private void OnAudioSourcePlay() =>
            _audioSourceUI.AudioSourceView.Play();
    }
}