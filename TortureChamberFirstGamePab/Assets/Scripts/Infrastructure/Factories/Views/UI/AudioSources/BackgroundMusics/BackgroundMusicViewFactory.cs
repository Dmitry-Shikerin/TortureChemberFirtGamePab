using System;
using Scripts.Controllers.UI.AudioSources.BackgroundMusics;
using Scripts.Infrastructure.Factories.Controllers.UI.AudioSources;
using Scripts.Presentation.UI.AudioSources.BackgroundMusics;
using Scripts.PresentationInterfaces.UI.AudioSources.BackgroundMusics;

namespace Scripts.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics
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