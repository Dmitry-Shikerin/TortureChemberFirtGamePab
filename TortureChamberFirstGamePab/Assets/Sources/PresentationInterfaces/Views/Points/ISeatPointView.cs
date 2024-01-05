using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

namespace Sources.PresentationInterfaces.Views.Points
{
    public interface ISeatPointView
    {
        public EatPointView EatPointView { get; }
        public bool IsOccupied { get; }

        public void SetIsOccupied(bool isOccupied);
    }
    
}