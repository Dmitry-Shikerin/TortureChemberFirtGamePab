using System;
using Sources.Controllers.UI.AudioSources.BackgroundMusics;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.UI.AudioSources.BackgroundMusics;

namespace Sources.Infrastructure.Factories.Controllers.UI.AudioSources
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
            return new BackgroundMusicPresenter(backgroundMusicView,
                _pauseService,
                _volumeService);
        }
    }
}