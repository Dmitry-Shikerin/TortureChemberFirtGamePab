using System;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.Presentation.UI.AudioSources;

namespace Sources.Infrastructure.Services
{
    public class BackgroundMusicService : IBackgroundMusicService
    {
        private readonly IVolumeService _volumeService;
        private readonly AudioSourceView _audioSourceView;

        public BackgroundMusicService
        (
            AudioSourceView audioSourceView,
            IVolumeService volumeService
        )
        {
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _audioSourceView = audioSourceView 
                ? audioSourceView 
                : throw new ArgumentNullException(nameof(audioSourceView));
        }

        public void Enter(object payload = null)
        {
            _volumeService.VolumeChanged += OnVolumeChanged;
            
            _audioSourceView.Play();
        }

        public void Exit()
        {
            _volumeService.VolumeChanged -= OnVolumeChanged;
            
            if (_audioSourceView == null)
                return;

            _audioSourceView.Stop();
        }

        private void OnVolumeChanged(float volume) => 
            _audioSourceView.SetVolume(volume);
    }
}