using System;
using Controllers.UI.AudioSources.BackgroundMusics;
using InfrastructureInterfaces.Services.PauseServices;
using InfrastructureInterfaces.Services.VolumeServices;
using PresentationInterfaces.UI.AudioSources.BackgroundMusics;

namespace Infrastructure.Factories.Controllers.UI.AudioSources
{
    public class BackgroundMusicPresenterFactory
    {
        private readonly IPauseService _pauseService;
        private readonly IVolumeService _volumeService;

        public BackgroundMusicPresenterFactory(
            IPauseService pauseService,
            IVolumeService volumeService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
        }

        public BackgroundMusicPresenter Create(IBackgroundMusicView backgroundMusicView)
        {
            return new BackgroundMusicPresenter(
                backgroundMusicView,
                _pauseService,
                _volumeService);
        }
    }
}