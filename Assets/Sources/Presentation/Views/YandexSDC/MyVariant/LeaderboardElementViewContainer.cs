using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Views.YandexSDC.MyVariant
{
    public class LeaderboardElementViewContainer : View
    {
        [field: SerializeField] public List<LeaderboardElementView> LeaderboardElementViews { get; private set; }
    }
}