using System;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.Views.Taverns.BackgroundMusics;
using UnityEngine;

namespace Sources.Controllers.Taverns
{
    public class BackgroundMusicPresenter : PresenterBase
    {
        private readonly IBackgroundMusicView _backgroundMusicView;
        private readonly IVolumeService _volumeService;
        private readonly IPauseService _pauseService;
        private readonly Volume _volume;

        public BackgroundMusicPresenter
        (
            Setting setting,
            IBackgroundMusicView backgroundMusicView,
            IVolumeService volumeService,
            IPauseService pauseService
        )
        {
            _backgroundMusicView = backgroundMusicView ??
                                   throw new ArgumentNullException(nameof(backgroundMusicView));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        //TODO прокинуть паузсервис во все аудиоСоурсы и сделать событие на паузу
        public override void Enable()
        {
            OnVolumeChanged(_volumeService.Volume);
            
            _volumeService.VolumeChanged += OnVolumeChanged;

            _backgroundMusicView.BackgroundAudioSource.SetLoop();
            _backgroundMusicView.BackgroundAudioSource.Play();

            _pauseService.PauseSoundActivated += OnPause;
            _pauseService.ContinueSoundActivated += OnContinue;
            Debug.Log("Background Music Play");
        }

        public override void Disable()
        {
            _volumeService.VolumeChanged -= OnVolumeChanged;
            
            _backgroundMusicView.BackgroundAudioSource.RemoveLoop();
            _backgroundMusicView.BackgroundAudioSource.Stop();
            
            Debug.Log("Background Music Stop Play");
        }

        private void OnPause()
        {
            _backgroundMusicView.BackgroundAudioSource.Pause();
            _backgroundMusicView.ButtonAudioSource.Pause();
        }

        private void OnContinue()
        {
            _backgroundMusicView.BackgroundAudioSource.UnPause();
            _backgroundMusicView.ButtonAudioSource.UnPause();
        }

        private void OnVolumeChanged(float volume)
        {
            _backgroundMusicView.BackgroundAudioSource.SetVolume(volume);
            _backgroundMusicView.ButtonAudioSource.SetVolume(volume);
        }
    }
}