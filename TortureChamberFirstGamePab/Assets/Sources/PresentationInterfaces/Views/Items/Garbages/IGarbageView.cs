using Sources.Presentation.UI.AudioSources;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Items.Garbages
{
    public interface IGarbageView : IView
    {
        AudioSourceUI AudioSourceUI { get; }
        float FillingRate { get; }
        IEatPointView EatPointView { get; }

        void SetEatPointView(IEatPointView eatPointView);
        void SetPosition(Vector3 position);
    }
}