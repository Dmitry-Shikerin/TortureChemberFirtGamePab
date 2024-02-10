using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class DoubleCallbackAudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IDoubleAudioSourceActivator _audioSourceActivator;
        private readonly IAudioSourceUI _audioSourceUI;

        public DoubleCallbackAudioSourceUIPresenter
        (
            IDoubleAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI
        )
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
        }

        public override void Enable()
        {
            _audioSourceUI.AudioSourceView.SetLoop();
            _audioSourceActivator.FirstAudioSourceActivated += OnPlaySound;
            _audioSourceActivator.SecondAudioSourceActivated += OnStopSound;
        }

        public override void Disable()
        {
            _audioSourceUI.AudioSourceView.RemoveLoop();
            _audioSourceActivator.FirstAudioSourceActivated -= OnPlaySound;
            _audioSourceActivator.SecondAudioSourceActivated -= OnStopSound;
        }

        private void OnStopSound() =>
            _audioSourceUI.AudioSourceView.Stop();

        private void OnPlaySound() =>
            _audioSourceUI.AudioSourceView.Play();
    }
}