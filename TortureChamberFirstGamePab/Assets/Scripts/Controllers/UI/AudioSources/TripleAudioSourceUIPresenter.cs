using System;
using Scripts.DomainInterfaces.UI.AudioSourcesActivators;
using Scripts.InfrastructureInterfaces.Services.VolumeServices;
using Scripts.PresentationInterfaces.UI.AudioSources;

namespace Scripts.Controllers.UI.AudioSources
{
    public class TripleAudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly ITripleAudioSourceActivator _audioSourceActivator;
        private readonly ITripleAudioSourceUI _audioSourceUI;
        private readonly IVolumeService _volumeService;

        public TripleAudioSourceUIPresenter(
            ITripleAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI,
            IVolumeService volumeService)
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public override void Enable()
        {
            _audioSourceActivator.FirstAudioSourceActivated += OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated += OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated += OnThirdAudioSourceActivate;

            _volumeService.VolumeChanged += OnVolumeChanged;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated -= OnThirdAudioSourceActivate;

            _volumeService.VolumeChanged -= OnVolumeChanged;
        }

        private void OnVolumeChanged()
        {
            _audioSourceUI.FirstAudioSourceView.SetVolume(_volumeService.Volume);
            _audioSourceUI.SecondAudioSourceView.SetVolume(_volumeService.Volume);
            _audioSourceUI.ThirdAudioSourceView.SetVolume(_volumeService.Volume);
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
    }
}