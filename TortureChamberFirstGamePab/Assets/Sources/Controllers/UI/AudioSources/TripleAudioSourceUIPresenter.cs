using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class TripleAudioSourceUIPresenter : PresenterBase
    {
        private readonly ITripleAudioSourceActivator _audioSourceActivator;
        private readonly ITripleAudioSourceUI _audioSourceUI;

        public TripleAudioSourceUIPresenter
        (
            ITripleAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI
        )
        {
            _audioSourceActivator = audioSourceActivator;
            _audioSourceUI = audioSourceUI;
        }

        public override void Enable()
        {
            _audioSourceActivator.FirstAudioSourceActivated += OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated += OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated += OnThirdAudioSourceActivate;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;
            _audioSourceActivator.ThirdAudioSourceActivated -= OnThirdAudioSourceActivate;
        }

        private void OnFirstAudioSourceActivate() => 
            _audioSourceUI.FirstAudioSourceView.Play();

        private void OnSecondAudioSourceActivate() => 
            _audioSourceUI.SecondAudioSourceView.Play();

        private void OnThirdAudioSourceActivate() => 
            _audioSourceUI.ThirdAudioSourceView.Play();
    }
}