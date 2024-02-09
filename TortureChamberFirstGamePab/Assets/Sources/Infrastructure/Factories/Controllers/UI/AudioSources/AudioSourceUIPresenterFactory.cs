using Sources.Controllers.UI.AudioSources;
using Sources.DomainInterfaces.UI;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Infrastructure.Factories.Controllers.UI.AudioSourceUIPresenters
{
    public class AudioSourceUIPresenterFactory
    {
        public AudioSourceUIPresenter Create
        (
            IAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI
        )
        {
            return new AudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public DoubleAudioSourceUIPresenter Create
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IDoubleAudioSourceUI audioSourceUI
        )
        {
            return new DoubleAudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public TripleAudioSourceUIPresenter Create
        (
            ITripleAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI
        )
        {
            return new TripleAudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public FourthAudioSourceUIPresenter Create
        (
            IFourthAudioSourceActivator audioSourceActivator,
            IFourthAudioSourceUI audioSourceUI
        )
        {
            return new FourthAudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }
    }
}