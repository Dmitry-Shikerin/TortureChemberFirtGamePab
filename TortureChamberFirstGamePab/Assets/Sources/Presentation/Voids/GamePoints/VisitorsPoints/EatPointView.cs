using Sources.Controllers.Points;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints
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
