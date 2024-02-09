using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class AudioSourceUIPresenter : PresenterBase
    {
        private readonly IAudioSourceActivator _audioSourceActivator;
        private readonly IAudioSourceUI _audioSourceUI;

        public AudioSourceUIPresenter(IAudioSourceActivator audioSourceActivator, IAudioSourceUI audioSourceUI)
        {
            _audioSourceActivator = audioSourceActivator ?? 
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
        }

        public override void Enable() => 
            _audioSourceActivator.AudioSourceActivated += OnAudioSourcePlay;

        public override void Disable() => 
            _audioSourceActivator.AudioSourceActivated -= OnAudioSourcePlay;

        private void OnAudioSourcePlay() => 
            _audioSourceUI.AudioSourceView.Play();
    }
}