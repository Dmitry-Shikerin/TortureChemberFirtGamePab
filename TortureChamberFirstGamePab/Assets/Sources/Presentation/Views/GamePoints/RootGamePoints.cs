using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI.Conteiners.AudioSources;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints
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
        //TODO куда поместить этот скрипт
    }
}
