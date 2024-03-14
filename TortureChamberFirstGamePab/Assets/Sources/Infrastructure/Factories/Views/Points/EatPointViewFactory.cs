using System;
using Sources.Domain.Points;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Infrastructure.Factories.Views.Points
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
            var eatPoint = new EatPoint();
            var eatPointPresenter = _eatPointPresenterFactory.Create(eatPoint, eatPointView);

            eatPointView.Construct(eatPointPresenter);

            return eatPointView;
        }
    }
}