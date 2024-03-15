using System;
using Scripts.Domain.Points;

namespace Scripts.Controllers.Points
{
    public class EatPointPresenter : PresenterBase
    {
        private readonly EatPoint _eatPoint;

        public EatPointPresenter(EatPoint eatPoint)
        {
            _eatPoint = eatPoint ?? throw new ArgumentNullException(nameof(eatPoint));
        }

        public bool IsClear => _eatPoint.IsClear;

        public void Clean() =>
            _eatPoint.Clean();

        public void SetDirty() =>
            _eatPoint.SetDirty();
    }
}