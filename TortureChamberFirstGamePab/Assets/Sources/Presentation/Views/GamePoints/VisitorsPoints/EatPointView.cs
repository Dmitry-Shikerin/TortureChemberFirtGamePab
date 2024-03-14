using Sources.Controllers.Points;
using Sources.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Sources.Presentation.Views.GamePoints.VisitorsPoints
{
    public class EatPointView : PresentableView<EatPointPresenter>, IEatPointView
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public bool IsClear => Presenter.IsClear;

        public void SetDirty()
        {
            Presenter.SetDirty();
        }

        public void Clean()
        {
            Presenter.Clean();
        }
    }
}