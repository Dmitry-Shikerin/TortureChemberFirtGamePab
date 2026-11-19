using System;
using Controllers.Points;
using Domain.Points;
using Infrastructure.Factories.Controllers.Points;
using Presentation.Views.GamePoints.VisitorsPoints;
using PresentationInterfaces.Views.Points;

namespace Infrastructure.Factories.Views.Points
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