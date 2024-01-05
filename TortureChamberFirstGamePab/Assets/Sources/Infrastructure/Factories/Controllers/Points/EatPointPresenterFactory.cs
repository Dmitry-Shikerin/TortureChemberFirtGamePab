using Sources.Controllers.Points;
using Sources.Domain;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

namespace Sources.Infrastructure.Factories.Controllers.Points
{
    public class EatPointPresenterFactory
    {
        public EatPointPresenter Create(IEatPointView eatPointView, EatPoint eatPoint)
        {
            return new EatPointPresenter
            (
                eatPointView,
                eatPoint
            );
        }
    }
}