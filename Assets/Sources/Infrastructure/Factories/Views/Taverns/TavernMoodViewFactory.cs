using System;
using Controllers.Taverns;
using Domain.Taverns;
using Infrastructure.Factories.Controllers.Taverns;
using Presentation.Views.Taverns;
using PresentationInterfaces.UI;
using PresentationInterfaces.Views.Taverns;

namespace Infrastructure.Factories.Views.Taverns
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