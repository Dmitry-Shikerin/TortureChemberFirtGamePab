using System;
using Sources.Controllers.UI.AudioSources;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Infrastructure.Factories.Controllers.UI.AudioSources
{
    public class AudioSourceUIPresenterFactory
    {
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public AudioSourceUIPresenterFactory(
            IPauseService pauseService,
            IVolumeService volumeService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public AudioSourceUIPresenter Create(
            IAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));

            return new AudioSourceUIPresenter(audioSourceActivator,
                audioSourceUI,
                _volumeService,
                _pauseService);
        }

        public DoubleAudioSourceUIPresenter Create(
            IDoubleAudioSourceActivator audioSourceActivator,
            IDoubleAudioSourceUI audioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));

            return new DoubleAudioSourceUIPresenter(audioSourceActivator,
                audioSourceUI,
                _pauseService,
                _volumeService);
        }

        public TripleAudioSourceUIPresenter Create(
            ITripleAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));

            return new TripleAudioSourceUIPresenter(audioSourceActivator,
                audioSourceUI,
                _volumeService);
        }

        public FourthAudioSourceUIPresenter Create(
            IFourthAudioSourceActivator audioSourceActivator,
            IFourthAudioSourceUI audioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));

            return new FourthAudioSourceUIPresenter(audioSourceActivator,
                audioSourceUI,
                _volumeService);
        }

        public DoubleCallbackAudioSourceUIPresenter Create(
            IDoubleAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (audioSourceUI == null)
                throw new ArgumentNullException(nameof(audioSourceUI));

            return new DoubleCallbackAudioSourceUIPresenter(audioSourceActivator,
                audioSourceUI,
                _pauseService,
                _volumeService);
        }

        public FourthCallBackAudioSourceUIPresenter Create(
            IFourthAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI tripleAudioSourceUI)
        {
            if (audioSourceActivator == null)
                throw new ArgumentNullException(nameof(audioSourceActivator));

            if (tripleAudioSourceUI == null)
                throw new ArgumentNullException(nameof(tripleAudioSourceUI));

            return new FourthCallBackAudioSourceUIPresenter(audioSourceActivator,
                tripleAudioSourceUI,
                _pauseService,
                _volumeService);
        }
    }
}