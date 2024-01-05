using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Garbages
{
    public interface IGarbageView
    {
        float FillingRate { get; }
        IEatPointView EatPointView { get; }

        void SetEatPointView(IEatPointView eatPointView);
        void SetPosition(Vector3 position);
        public void Destroy();
    }
}