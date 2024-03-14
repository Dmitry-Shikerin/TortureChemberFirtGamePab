using System;
using Sources.Controllers.Points;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces;
using Sources.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Sources.Presentation.Views.GamePoints.VisitorsPoints
{
    public class SeatPointView : PresentableView<SeatPointPresenter>, ISeatPointView, IVisitorPoint
    {
        private void Awake()
        {
            EatPointView = GetComponentInChildren<EatPointView>() ??
                           throw new NullReferenceException(nameof(EatPointView));
        }

        public EatPointView EatPointView { get; private set; }
        public bool IsOccupied => Presenter.IsOccupied;

        public void Occupy()
        {
            Presenter.Occupy();
        }

        public void UnOccupy()
        {
            Presenter.UnOccupy();
        }

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}