using System;
using JetBrains.Annotations;
using Sources.Controllers.Points;
using Sources.Domain;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

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
            EatPoint eatPoint = new EatPoint();
            EatPointPresenter eatPointPresenter = new EatPointPresenter(eatPointView, eatPoint);
            
            eatPointView.Construct(eatPointPresenter);

            return eatPointView;
        }
    }
}