using System;
using Controllers.UI.AudioSources;
using DomainInterfaces.UI.AudioSourcesActivators;
using Infrastructure.Factories.Controllers.UI.AudioSources;
using Presentation.UI.AudioSources;
using PresentationInterfaces.UI.AudioSources;

namespace Infrastructure.Factories.Views.UI.AudioSources
{
    public class AudioSourceUIFactory
    {
        private readonly AudioSourceUIPresenterFactory _presenterFactory;

        public AudioSourceUIFactory(AudioSourceUIPresenterFactory presenterFactory)
        {
            _presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));
        }

        public IAudioSourceUI Create(
            IAudioSourceActivator audioSourceActivator,
            AudioSourceUI audioSourceUI)
        {
            AudioSourceUIPresenter audioSourceUIPresenter =
                _presenterFactory.Create(audioSourceActivator, audioSourceUI);

            audioSourceUI.Construct(audioSourceUIPresenter);

            return audioSourceUI;
        }

        public IDoubleAudioSourceUI Create(
            IDoubleAudioSourceActivator audioSourceActivator,
            DoubleAudioSourceUI audioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));

            DoubleAudioSourceUIPresenter audioSourceUIPresenter =
                _presenterFactory.Create(audioSourceActivator, audioSourceUI);

            audioSourceUI.Construct(audioSourceUIPresenter);

            return audioSourceUI;
        }

        public ITripleAudioSourceUI Create(
            ITripleAudioSourceActivator audioSourceActivator,
            TripleAudioSourceUI audioSourceUI)
        {
            TripleAudioSourceUIPresenter audioSourceUIPresenter =
                _presenterFactory.Create(audioSourceActivator, audioSourceUI);

            audioSourceUI.Construct(audioSourceUIPresenter);

            return audioSourceUI;
        }

        public IFourthAudioSourceUI Create(
            IFourthAudioSourceActivator audioSourceActivator,
            FourthAudioSourceUI audioSourceUI)
        {
            FourthAudioSourceUIPresenter audioSourceUIPresenter =
                _presenterFactory.Create(audioSourceActivator, audioSourceUI);

            audioSourceUI.Construct(audioSourceUIPresenter);

            return audioSourceUI;
        }

        public IAudioSourceUI Create(
            IDoubleAudioSourceActivator audioSourceActivator,
            AudioSourceUI audioSourceUI)
        {
            DoubleCallbackAudioSourceUIPresenter audioSourceUIPresenter =
                _presenterFactory.Create(audioSourceActivator, audioSourceUI);

            audioSourceUI.Construct(audioSourceUIPresenter);

            return audioSourceUI;
        }

        public ITripleAudioSourceUI Create(
            IFourthAudioSourceActivator audioSourceActivator,
            TripleAudioSourceUI tripleAudioSourceUI)
        {
            FourthCallBackAudioSourceUIPresenter fourthCallBackAudioSourceUIPresenter =
                _presenterFactory.Create(audioSourceActivator, tripleAudioSourceUI);

            tripleAudioSourceUI.Construct(fourthCallBackAudioSourceUIPresenter);

            return tripleAudioSourceUI;
        }
    }
}