using System;
using Scripts.Controllers.Taverns;
using Scripts.Domain.Taverns;
using Scripts.Infrastructure.Factories.Controllers.Taverns;
using Scripts.Presentation.Views.Taverns;
using Scripts.PresentationInterfaces.UI;
using Scripts.PresentationInterfaces.Views.Taverns;

namespace Scripts.Infrastructure.Factories.Views.Taverns
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

            TavernMoodPresenter tavernMoodPresenter =
                _tavernMoodPresenterFactory.Create(tavernMood, tavernMoodView, imageUI);
            tavernMoodView.Construct(tavernMoodPresenter);

            return tavernMoodView;
        }
    }
}