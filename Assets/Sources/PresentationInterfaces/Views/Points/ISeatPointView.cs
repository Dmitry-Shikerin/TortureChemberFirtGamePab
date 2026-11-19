using Presentation.Views.GamePoints.VisitorsPoints;

namespace PresentationInterfaces.Views.Points
{
    public interface ISeatPointView
    {
        public EatPointView EatPointView { get; }
        public bool IsOccupied { get; }

        void Occupy();
        void UnOccupy();
    }
}