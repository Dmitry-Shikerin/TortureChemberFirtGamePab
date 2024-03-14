using System;
using Sources.Controllers.UI;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.UI;

namespace Sources.Infrastructure.Factories.Controllers.UI
{
    public class ImageUIPresenterFactory
    {
        private readonly IPauseService _pauseService;

        public ImageUIPresenterFactory(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public ImageUIPresenter Create(IImageUI imageUI)
        {
            if (imageUI == null)
                throw new ArgumentNullException(nameof(imageUI));

            return new ImageUIPresenter(imageUI, _pauseService);
        }
    }
}