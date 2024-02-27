using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class FourthAudioSourceUIPresenter : PresenterBase
    {
        private readonly IFourthAudioSourceActivator _audioSourceActivator;
        private readonly IFourthAudioSourceUI _audioSourceUI;
        private readonly IVolumeService _volumeService;
        private readonly IPauseService _pauseService;

        public FourthAudioSourceUIPresenter
        (
            IFourthAudioSourceActivator audioSourceActivator,
            IFourthAudioSourceUI audioSourceUI,
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
            _audioSourceActivator.ThirdAudioSourceActivated += OnThirdAudioSourceActivate;
            _audioSourceActivator.FourthAudioSourceActivated += OnFourthAudioSourceActivate;

            _volumeService.VolumeChanged += OnVolumeChanged;

            _pauseService.PauseActivated += OnPause;
            _pauseService.ContinueActivated += OnContinue;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated -= OnThirdAudioSourceActivate;
            _audioSourceActivator.FourthAudioSourceActivated -= OnFourthAudioSourceActivate;

            _volumeService.VolumeChanged -= OnVolumeChanged;
        }

        private void OnPause()
        {
            _audioSourceUI.FirstAudioSourceView.Pause();
            _audioSourceUI.SecondAudioSourceView.Pause();
            _audioSourceUI.ThirdAudioSourceView.Pause();
            _audioSourceUI.FourthAudioSourceView.UnPause();
        }

        private void OnContinue()
        {
            _audioSourceUI.FirstAudioSourceView.UnPause();
            _audioSourceUI.SecondAudioSourceView.UnPause();
            _audioSourceUI.ThirdAudioSourceView.UnPause();
            _audioSourceUI.FourthAudioSourceView.UnPause();
        }

        private void OnVolumeChanged(float volume)
        {
            _audioSourceUI.FirstAudioSourceView.SetVolume(volume);
            _audioSourceUI.SecondAudioSourceView.SetVolume(volume);
            _audioSourceUI.ThirdAudioSourceView.SetVolume(volume);
            _audioSourceUI.FourthAudioSourceView.SetVolume(volume);
        }

        private void OnFirstAudioSourceActivate() => 
            _audioSourceUI.FirstAudioSourceView.Play();

        private void OnSecondAudioSourceActivate() => 
            _audioSourceUI.SecondAudioSourceView.Play();

        private void OnThirdAudioSourceActivate() => 
            _audioSourceUI.ThirdAudioSourceView.Play();

        private void OnFourthAudioSourceActivate() => 
            _audioSourceUI.FourthAudioSourceView.Play();
    }
}