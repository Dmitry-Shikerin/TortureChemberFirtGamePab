using System;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources.BackgroundMusics;
using UnityEngine;

namespace Sources.Controllers.UI.AudioSources.BackgroundMusics
{
    public class BackgroundMusicPresenter : PresenterBase
    {
        private readonly IBackgroundMusicView _backgroundMusicView;
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public BackgroundMusicPresenter
        (
            IBackgroundMusicView backgroundMusicView,
            IPauseService pauseService,
            IVolumeService volumeService
        )
        {
            _backgroundMusicView = backgroundMusicView ??
                                   throw new ArgumentNullException(nameof(backgroundMusicView));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public override void Enable()
        {
            OnVolumeChanged();

            _volumeService.VolumeChanged += OnVolumeChanged;

            _pauseService.PauseSoundActivated += OnPauseSound;
            _pauseService.ContinueSoundActivated += OnContinueSound;
            
            _backgroundMusicView.BackgroundMusic.SetLoop();
            _backgroundMusicView.BackgroundMusic.Play();
            
            Debug.Log("BackgroundMusic Enable");
        }

        public override void Disable()
        {
            _volumeService.VolumeChanged -= OnVolumeChanged;
            
            _pauseService.PauseSoundActivated -= OnPauseSound;
            _pauseService.ContinueSoundActivated -= OnContinueSound;
            
            _backgroundMusicView.BackgroundMusic.RemoveLoop();
            _backgroundMusicView.BackgroundMusic.Stop();
            
            Debug.Log("BackgroundMusic Disable");
        }
        
        private void OnPauseSound()
        {
            _backgroundMusicView.BackgroundMusic.Pause();
            _backgroundMusicView.ButtonSound.Pause();
        }

        private void OnContinueSound()
        {
            _backgroundMusicView.BackgroundMusic.Continue();
            _backgroundMusicView.ButtonSound.Continue();
        }

        private void OnVolumeChanged()
        {
            _backgroundMusicView.BackgroundMusic.SetVolume(_volumeService.Volume);
            _backgroundMusicView.ButtonSound.SetVolume(_volumeService.Volume);
        }
    }
}