using System;
using Sources.Domain.Points;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Controllers.Points
{
    public class EatPointPresenter : PresenterBase
    {
        private readonly EatPoint _eatPoint;
        private readonly IEatPointView _eatPointView;

        public EatPointPresenter(EatPoint eatPoint, IEatPointView eatPointView)
        {
            _eatPointView = eatPointView ?? throw new ArgumentNullException(nameof(eatPointView));
            _eatPoint = eatPoint ?? throw new ArgumentNullException(nameof(eatPoint));
        }

        public bool IsClear => _eatPoint.IsClear;

        public void Clean()
        {
            _eatPoint.Clean();
        }

        public void SetDirty()
        {
            _eatPoint.SetDirty();
        }
    }
}