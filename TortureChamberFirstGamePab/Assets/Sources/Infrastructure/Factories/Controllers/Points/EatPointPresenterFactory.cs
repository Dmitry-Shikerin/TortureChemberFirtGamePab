using System;
using Sources.Controllers.Points;
using Sources.Domain.Points;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

namespace Sources.Infrastructure.Factories.Controllers.Points
{
    public class EatPointPresenterFactory
    {
        public EatPointPresenter Create(EatPoint eatPoint, IEatPointView eatPointView)
        {
            if (eatPointView == null) 
                throw new ArgumentNullException(nameof(eatPointView));
            
            if (eatPoint == null) 
                throw new ArgumentNullException(nameof(eatPoint));
            
            return new EatPointPresenter
            (
                eatPoint,
                eatPointView
            );
        }
    }
}