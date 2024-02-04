using System.Collections.Generic;
using UnityEngine;

namespace Sources.Presentation.Views.YandexSDC.MyVariant
{
    public class LeaderboardElementViewContainer : View
    {
        [field: SerializeField] public List<LeaderboardElementView> LeaderboardElementViews { get; private set; }
    }
}