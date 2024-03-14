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
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public DoubleAudioSourceUIPresenter(
            IDoubleAudioSourceActivator audioSourceActivator,
            IDoubleAudioSourceUI audioSourceUI,
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

        private void OnPause()
        {
            _audioSourceUI.FirstAudioSourceView.Pause();
            _audioSourceUI.SecondAudioSourceView.Pause();
        }

        private void OnContinue()
        {
            _audioSourceUI.FirstAudioSourceView.Continue();
            _audioSourceUI.SecondAudioSourceView.Continue();
        }

        private void OnVolumeChanged()
        {
            _audioSourceUI.FirstAudioSourceView.SetVolume(_volumeService.Volume);
            _audioSourceUI.SecondAudioSourceView.SetVolume(_volumeService.Volume);
        }

        private void OnFirstAudioSourceActivate()
        {
            _audioSourceUI.FirstAudioSourceView.Play();
        }

        private void OnSecondAudioSourceActivate()
        {
            _audioSourceUI.SecondAudioSourceView.Play();
        }
    }
}