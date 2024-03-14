using System;
using Sources.Domain.Taverns;
using Sources.Infrastructure.Factories.Controllers.Taverns;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Infrastructure.Factories.Views.Taverns
{
    public class TavernMoodViewFactory
    {
        private readonly TavernMoodPresenterFactory _tavernMoodPresenterFactory;

        public TavernMoodViewFactory(TavernMoodPresenterFactory tavernMoodPresenterFactory)
        {
            _tavernMoodPresenterFactory = tavernMoodPresenterFactory ??
                                          throw new ArgumentNullException(nameof(tavernMoodPresenterFactory));
        }

        public ITavernMoodView Create(
            TavernMoodView tavernMoodView,
            TavernMood tavernMood,
            IImageUI imageUI)
        {
            if (tavernMoodView == null)
                throw new ArgumentNullException(nameof(tavernMoodView));

            if (tavernMood == null)
                throw new ArgumentNullException(nameof(tavernMood));

            if (imageUI == null)
                throw new ArgumentNullException(nameof(imageUI));

            var tavernMoodPresenter =
                _tavernMoodPresenterFactory.Create(tavernMood, tavernMoodView, imageUI);
            tavernMoodView.Construct(tavernMoodPresenter);

            return tavernMoodView;
        }
    }
}