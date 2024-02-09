using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class DoubleAudioSourceUIPresenter : PresenterBase
    {
        private readonly IDoubleAudioSourceActivator _audioSourceActivator;
        private readonly IDoubleAudioSourceUI _audioSourceUI;

        public DoubleAudioSourceUIPresenter
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IDoubleAudioSourceUI audioSourceUI
        )
        {
            _audioSourceActivator = audioSourceActivator;
            _audioSourceUI = audioSourceUI;
        }

        public override void Enable()
        {
            _audioSourceActivator.FirstAudioSourceActivated += OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated += OnSecondAudioSourceActivate;
        }

        public override void Disable()
        {
            _audioSourceActivator.FirstAudioSourceActivated -= OnFirstAudioSourceActivate;
            _audioSourceActivator.SecondAudioSourceActivated -= OnSecondAudioSourceActivate;
        }

        private void OnFirstAudioSourceActivate() => 
            _audioSourceUI.FirstAudioSourceView.Play();

        private void OnSecondAudioSourceActivate() => 
            _audioSourceUI.SecondAudioSourceView.Play();
    }
}