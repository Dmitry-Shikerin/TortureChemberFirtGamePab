using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class FourthAudioSourceUIPresenter : PresenterBase
    {
        private readonly IFourthAudioSourceActivator _audioSourceActivator;
        private readonly IFourthAudioSourceUI _audioSourceUI;
        private readonly IVolumeService _volumeService;

        public FourthAudioSourceUIPresenter(
            IFourthAudioSourceActivator audioSourceActivator,
            IFourthAudioSourceUI audioSourceUI,
            IVolumeService volumeService)
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public override void Enable()
        {
            OnVolumeChanged();

            _audioSourceActivator.FirstAudioSourceActivated += OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated += OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated += OnThirdAudioSourceActivate;
            _audioSourceActivator.FourthAudioSourceActivated += OnFourthAudioSourceActivate;

            _volumeService.VolumeChanged += OnVolumeChanged;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated -= OnThirdAudioSourceActivate;
            _audioSourceActivator.FourthAudioSourceActivated -= OnFourthAudioSourceActivate;

            _volumeService.VolumeChanged -= OnVolumeChanged;
        }

        private void OnVolumeChanged()
        {
            _audioSourceUI.FirstAudioSourceView.SetVolume(_volumeService.Volume);
            _audioSourceUI.SecondAudioSourceView.SetVolume(_volumeService.Volume);
            _audioSourceUI.ThirdAudioSourceView.SetVolume(_volumeService.Volume);
            _audioSourceUI.FourthAudioSourceView.SetVolume(_volumeService.Volume);
        }

        private void OnFirstAudioSourceActivate()
        {
            _audioSourceUI.FirstAudioSourceView.Play();
        }

        private void OnSecondAudioSourceActivate()
        {
            _audioSourceUI.SecondAudioSourceView.Play();
        }

        private void OnThirdAudioSourceActivate()
        {
            _audioSourceUI.ThirdAudioSourceView.Play();
        }

        private void OnFourthAudioSourceActivate()
        {
            _audioSourceUI.FourthAudioSourceView.Play();
        }
    }
}