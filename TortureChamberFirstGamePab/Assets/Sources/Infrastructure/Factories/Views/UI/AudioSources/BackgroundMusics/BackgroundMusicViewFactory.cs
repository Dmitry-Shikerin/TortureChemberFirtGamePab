using System;
using Sources.Controllers.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Factories.Controllers.UI.AudioSources;
using Sources.Presentation.UI.AudioSources.BackgroundMusics;
using Sources.PresentationInterfaces.UI.AudioSources.BackgroundMusics;

namespace Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics
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