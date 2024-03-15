using System;
using Scripts.Controllers.Points;
using Scripts.Domain.Points;
using Scripts.PresentationInterfaces.Views.Points;

namespace Scripts.Infrastructure.Factories.Controllers.Points
{
    public class EatPointPresenterFactory
    {
        public EatPointPresenter Create(EatPoint eatPoint, IEatPointView eatPointView)
        {
            if (eatPointView == null)
                throw new ArgumentNullException(nameof(eatPointView));

            if (eatPoint == null)
                throw new ArgumentNullException(nameof(eatPoint));

            return new EatPointPresenter(eatPoint);
        }
    }
}