using Sources.Presentation.Views;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints
{
    public class RootGamePoints : View
    {
        [field: SerializeField] public VisitorPoints VisitorPoints { get; private set; }
    }
}
