using System;
using Sources.Controllers.Taverns;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.PresentationInterfaces.Views.Taverns.BackgroundMusics;

namespace Sources.Infrastructure.Factories.Controllers.Taverns
{
    public class BackgroundMusicPresenterFactory
    {
        private readonly Setting _setting;
        private readonly IVolumeService _volumeService;
        private readonly IPauseService _pauseService;

        public BackgroundMusicPresenterFactory
        (
            Setting setting,
            IVolumeService volumeService,
            IPauseService pauseService
        )
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public BackgroundMusicPresenter Create(IBackgroundMusicView backgroundMusicView)
        {
            return new BackgroundMusicPresenter(_setting, backgroundMusicView, 
                _volumeService, _pauseService);
        }
    }
}