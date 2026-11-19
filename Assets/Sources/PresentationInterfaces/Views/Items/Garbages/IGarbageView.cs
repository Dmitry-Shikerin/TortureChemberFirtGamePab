using Presentation.UI.AudioSources;
using PresentationInterfaces.Views.Points;
using UnityEngine;

namespace PresentationInterfaces.Views.Items.Garbages
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