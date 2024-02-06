using System;
using Sources.Controllers.Points;
using Sources.Presentation.Views;
using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces;
using Sources.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints
{
    public class SeatPointView : PresentableView<SeatPointPresenter>,ISeatPointView, IVisitorPoint
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public EatPointView EatPointView { get; private set; }
        public bool IsOccupied => Presenter.IsOccupied;

        private void Awake()
        {
            EatPointView = GetComponentInChildren<EatPointView>() ?? 
                       throw new NullReferenceException(nameof(EatPointView));
        }

        public void Occupy() => 
            Presenter.Occupy();

        public void UnOccupy() => 
            Presenter.UnOccupy();
    }
}
