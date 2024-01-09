using System;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.PresentationInterfaces.Views.Points;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
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
