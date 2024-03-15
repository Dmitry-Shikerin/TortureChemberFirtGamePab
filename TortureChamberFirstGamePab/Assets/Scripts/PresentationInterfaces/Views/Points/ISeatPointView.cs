using Scripts.Presentation.Views.GamePoints.VisitorsPoints;

namespace Scripts.PresentationInterfaces.Views.Points
{
    public interface ISeatPointView
    {
        public EatPointView EatPointView { get; }
        public bool IsOccupied { get; }

        void Occupy();
        void UnOccupy();
    }
}