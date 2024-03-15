using System;
using Scripts.Controllers.Points;
using Scripts.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Scripts.Presentation.Views.GamePoints.VisitorsPoints
{
    public class SeatPointView : PresentableView<SeatPointPresenter>, ISeatPointView, IVisitorPoint
    {
        public EatPointView EatPointView { get; private set; }
        public bool IsOccupied => Presenter.IsOccupied;
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void Awake() =>
            EatPointView = GetComponentInChildren<EatPointView>() ??
                           throw new NullReferenceException(nameof(EatPointView));

        public void Occupy() =>
            Presenter.Occupy();

        public void UnOccupy() =>
            Presenter.UnOccupy();
    }
}