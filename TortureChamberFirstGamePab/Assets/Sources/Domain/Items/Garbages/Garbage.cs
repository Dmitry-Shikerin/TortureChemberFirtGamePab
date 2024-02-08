using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Domain.Items.Garbages
{
    public class Garbage
    {
        public IEatPointView EatPointView { get; private set; }
        
        public void SetEatPointView(IEatPointView eatPointView) => 
            EatPointView = eatPointView;
    }
}