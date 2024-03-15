using System;
using Scripts.Controllers.Points;
using Scripts.Domain.Points;
using Scripts.Infrastructure.Factories.Controllers.Points;
using Scripts.Presentation.Views.GamePoints.VisitorsPoints;
using Scripts.PresentationInterfaces.Views.Points;

namespace Scripts.Infrastructure.Factories.Views.Points
{
    public class EatPointViewFactory
    {
        private readonly EatPointPresenterFactory _eatPointPresenterFactory;

        public EatPointViewFactory(EatPointPresenterFactory eatPointPresenterFactory)
        {
            _eatPointPresenterFactory = eatPointPresenterFactory ??
                                        throw new ArgumentNullException(nameof(eatPointPresenterFactory));
        }

        public IEatPointView Create(EatPointView eatPointView)
        {
            EatPoint eatPoint = new EatPoint();
            EatPointPresenter eatPointPresenter = _eatPointPresenterFactory.Create(eatPoint, eatPointView);

            eatPointView.Construct(eatPointPresenter);

            return eatPointView;
        }
    }
}