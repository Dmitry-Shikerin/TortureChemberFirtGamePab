using MyProject.Sources.Controllers.Common;
using Sources.Controllers.Points;
using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints
{
    public class EatPointView : PresentableView<EatPointPresenter>, IEatPointView
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    
        //TODO это должно быть в модели
        public bool IsClear => Presenter.IsClear;

        public void SetIsClean(bool isClean)
        {
            Presenter.SetIsClean(isClean);
        }
    }
}
