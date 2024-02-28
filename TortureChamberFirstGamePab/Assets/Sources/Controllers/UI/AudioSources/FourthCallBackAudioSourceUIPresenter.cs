using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Controllers.UI.AudioSources
{
    public class FourthCallBackAudioSourceUIPresenter : AudioSourceUIPresenterBase
    {
        private readonly IFourthAudioSourceActivator _audioSourceActivator;
        private readonly ITripleAudioSourceUI _audioSourceUI;
        private readonly IPauseService _pauseService;

        public FourthCallBackAudioSourceUIPresenter
        (
            IFourthAudioSourceActivator audioSourceActivator,
            ITripleAudioSourceUI audioSourceUI,
            IPauseService pauseService
        )
        {
            _audioSourceActivator = audioSourceActivator ??
                                    throw new ArgumentNullException(nameof(audioSourceActivator));
            _audioSourceUI = audioSourceUI ?? throw new ArgumentNullException(nameof(audioSourceUI));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public override void Enable()
        {
            _audioSourceUI.ThirdAudioSourceView.SetLoop();

            _audioSourceActivator.FirstAudioSourceActivated += OnPlayFirstSound;
            _audioSourceActivator.SecondAudioSourceActivated += OnPlaySecondSound;
            
            _audioSourceActivator.ThirdAudioSourceActivated += OnPlaySound;
            _audioSourceActivator.FourthAudioSourceActivated += OnStopSound;

            _pauseService.PauseActivated += OnPauseSound;
            _pauseService.ContinueActivated += OnContinueSound;
        }

        public override void Disable()
        {
            _audioSourceUI.ThirdAudioSourceView.RemoveLoop();
            
            _audioSourceActivator.FirstAudioSourceActivated -= OnPlayFirstSound;
            _audioSourceActivator.SecondAudioSourceActivated -= OnPlaySecondSound;
            
            _audioSourceActivator.ThirdAudioSourceActivated -= OnPlaySound;
            _audioSourceActivator.FourthAudioSourceActivated -= OnStopSound;
            
            _pauseService.PauseActivated -= OnPauseSound;
            _pauseService.ContinueActivated -= OnContinueSound;
        }

        private void OnPlayFirstSound() => 
            _audioSourceUI.FirstAudioSourceView.Play();

        private void OnPlaySecondSound() => 
            _audioSourceUI.SecondAudioSourceView.Play();

        private void OnPlaySound() =>
            _audioSourceUI.ThirdAudioSourceView.Play();

        private void OnStopSound() =>
            _audioSourceUI.ThirdAudioSourceView.Stop();

        private void OnPauseSound()
        {
            if(_audioSourceActivator.IsActive == false)
                return;
            
            _audioSourceUI.ThirdAudioSourceView.Pause();
        }

        private void OnContinueSound()
        {
            if(_audioSourceActivator.IsActive == false)
                return;
            
            _audioSourceUI.ThirdAudioSourceView.Continue();
        }

    }
}