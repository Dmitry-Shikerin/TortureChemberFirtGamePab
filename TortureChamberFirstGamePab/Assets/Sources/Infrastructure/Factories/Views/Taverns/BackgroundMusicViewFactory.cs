using System;
using Sources.Controllers.Taverns;
using Sources.Infrastructure.Factories.Controllers.Taverns;
using Sources.Presentation.Views.Taverns.BackGroundMusics;
using Sources.PresentationInterfaces.Views.Taverns.BackgroundMusics;

namespace Sources.Infrastructure.Factories.Views.Taverns
{
    public class BackgroundMusicViewFactory
    {
        private readonly BackgroundMusicPresenterFactory _backgroundMusicPresenterFactory;

        public BackgroundMusicViewFactory(BackgroundMusicPresenterFactory backgroundMusicPresenterFactory)
        {
            _backgroundMusicPresenterFactory = 
                backgroundMusicPresenterFactory 
                ?? throw new ArgumentNullException(nameof(backgroundMusicPresenterFactory));
        }

        public IBackgroundMusicView Create(BackgroundMusicView backgroundMusicView)
        {
            BackgroundMusicPresenter backgroundMusicPresenter =
                _backgroundMusicPresenterFactory.Create(backgroundMusicView);
            
            backgroundMusicView.Construct(backgroundMusicPresenter);

            return backgroundMusicView;
        }
    }
}