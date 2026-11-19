using Presentation.Containers.UI.AudioSources;
using Presentation.Triggers.Taverns;
using Presentation.Views;
using Presentation.Views.GamePoints.VisitorsPoints;
using Presentation.Views.Player;
using Presentation.Views.Taverns.PickUpPoints.Foods;
using UnityEngine;

namespace Presentation.Containers.GamePoints
{
    public class RootGamePoints : View
    {
        [field: SerializeField] public VisitorPoints VisitorPoints { get; private set; }
        [field: SerializeField] public BeerPickUpPointView BeerPickUpPointView { get; private set; }
        [field: SerializeField] public BreadPickUpPointView BreadPickUpPointView { get; private set; }
        [field: SerializeField] public WinePickUpPointView WinePickUpPointView { get; private set; }
        [field: SerializeField] public SoupPickUpPointView SoupPickUpPointView { get; private set; }
        [field: SerializeField] public MeatPickUpPointView MeatPickUpPointView { get; private set; }
        [field: SerializeField] public TavernUpgradeTrigger TavernUpgradeTrigger { get; private set; }
        [field: SerializeField] public UpgradePointsInteractionAudioSourceContainer
            UpgradePointsInteractionAudioSourceContainer { get; private set; }
        [field: SerializeField] public PlayerSpawnPoint PlayerSpawnPoint { get; private set; }
    }
}