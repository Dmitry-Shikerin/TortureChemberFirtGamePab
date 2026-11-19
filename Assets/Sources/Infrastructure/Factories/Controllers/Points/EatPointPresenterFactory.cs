using System;
using Controllers.Points;
using Domain.Points;
using PresentationInterfaces.Views.Points;

namespace Infrastructure.Factories.Controllers.Points
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