using Scripts.Presentation.Containers.UI.AudioSources;
using Scripts.Presentation.Triggers.Taverns;
using Scripts.Presentation.Views;
using Scripts.Presentation.Views.GamePoints.VisitorsPoints;
using Scripts.Presentation.Views.Player;
using Scripts.Presentation.Views.Taverns.PickUpPoints.Foods;
using UnityEngine;

namespace Scripts.Presentation.Containers.GamePoints
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