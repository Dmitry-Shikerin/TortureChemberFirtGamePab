using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class FourthAudioSourceUIPresenter : PresenterBase
    {
        private readonly IFourthAudioSourceActivator _audioSourceActivator;
        private readonly IFourthAudioSourceUI _audioSourceUI;

        public FourthAudioSourceUIPresenter
        (
            IFourthAudioSourceActivator audioSourceActivator,
            IFourthAudioSourceUI audioSourceUI
        )
        {
            _audioSourceActivator = audioSourceActivator ?? 
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
        }

        public override void Enable()
        {
            _audioSourceActivator.FirstAudioSourceActivated += OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated += OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated += OnThirdAudioSourceActivate;
            _audioSourceActivator.FourthAudioSourceActivated += OnFourthAudioSourceActivate;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated -= OnThirdAudioSourceActivate;
            _audioSourceActivator.FourthAudioSourceActivated -= OnFourthAudioSourceActivate;
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