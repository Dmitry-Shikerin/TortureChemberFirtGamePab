using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Items.Garbages
{
    public interface IGarbageView : IView
    {
        float FillingRate { get; }
        IEatPointView EatPointView { get; }

        void SetEatPointView(IEatPointView eatPointView);
        void SetPosition(Vector3 position);
    }
}