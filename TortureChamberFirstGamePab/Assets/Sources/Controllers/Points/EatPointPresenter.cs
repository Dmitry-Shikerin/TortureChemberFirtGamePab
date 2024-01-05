using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

namespace Sources.Controllers.Points
{
    public class EatPointPresenter : PresenterBase
    {
        private readonly IEatPointView _eatPointView;
        private readonly EatPoint _eatPoint;

        public EatPointPresenter(IEatPointView eatPointView, EatPoint eatPoint)
        {
            _eatPointView = eatPointView ?? throw new ArgumentNullException(nameof(eatPointView));
            _eatPoint = eatPoint ?? throw new ArgumentNullException(nameof(eatPoint));
        }

        public bool IsClear => _eatPoint.IsClear;

        public void SetIsClean(bool isClean)
        {
            _eatPoint.SetIsClear(isClean);
        }
    }
}