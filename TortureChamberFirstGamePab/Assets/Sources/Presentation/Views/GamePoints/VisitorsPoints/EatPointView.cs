using Sources.Controllers.Points;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using UnityEngine;

namespace Sources.Presentation.Views.GamePoints.VisitorsPoints
{
    public class EatPointView : PresentableView<EatPointPresenter>, IEatPointView
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public bool IsClear => Presenter.IsClear;
        
        public void GetDirty() => 
            Presenter.GetDirty();

        public void Clean() => 
            Presenter.Clean();
    }
}
