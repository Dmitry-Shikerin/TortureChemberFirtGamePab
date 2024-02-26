using System;
using Sources.Controllers.UI.AudioSources;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Infrastructure.Factories.Controllers.UI.AudioSources
{
    public class AudioSourceUIPresenterFactory
    {
        private readonly IPauseService _pauseService;

        public AudioSourceUIPresenterFactory(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public AudioSourceUIPresenter Create
        (
            IAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI
        )
        {
            if (audioSourceActivator == null) 
                throw new ArgumentNullException(nameof(audioSourceActivator));
            
            if (audioSourceUI == null) 
                throw new ArgumentNullException(nameof(audioSourceUI));
            
            return new AudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public DoubleAudioSourceUIPresenter Create
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IDoubleAudioSourceUI audioSourceUI
        )
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));
            
            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));
            
            return new DoubleAudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public TripleAudioSourceUIPresenter Create
        (
            ITripleAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI
        )
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));
            
            if (audioSourceUI == null) 
                throw new ArgumentNullException(nameof(audioSourceUI));
            
            return new TripleAudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public FourthAudioSourceUIPresenter Create
        (
            IFourthAudioSourceActivator audioSourceActivator,
            IFourthAudioSourceUI audioSourceUI
        )
        {
            if (audioSourceActivator == null) 
                throw new ArgumentNullException(nameof(audioSourceActivator));
            
            if (audioSourceUI == null) 
                throw new ArgumentNullException(nameof(audioSourceUI));
            
            return new FourthAudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }

        public DoubleCallbackAudioSourceUIPresenter Create
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI
        )
        {
            if (audioSourceActivator == null) 
                throw new ArgumentNullException(nameof(audioSourceActivator));
            
            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));
            
            return new DoubleCallbackAudioSourceUIPresenter(audioSourceActivator, audioSourceUI, _pauseService);
        }

        public FourthCallBackAudioSourceUIPresenter Create
        (
            IFourthAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI tripleAudioSourceUI
        )
        {
            if (audioSourceActivator == null) 
                throw new ArgumentNullException(nameof(audioSourceActivator));
            
            if (tripleAudioSourceUI == null)
                throw new ArgumentNullException(nameof(tripleAudioSourceUI));

            return new FourthCallBackAudioSourceUIPresenter(audioSourceActivator, tripleAudioSourceUI, _pauseService);
        }
    }
}