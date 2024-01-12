using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointTextUIs : MonoBehaviour
    {
        [field : SerializeField] public TextUI PlayerCharismaLevelUpgradeTextUI { get; private set; }
        [field : SerializeField] public TextUI PlayerCharismaPriceNextLevelUpgradeTextUI { get; private set; }
        [field : SerializeField] public TextUI PlayerInventoryLevelUpgradeTextUI { get; private set; }
        [field : SerializeField] public TextUI PlayerInventoryPriceNextLevelUpgradeTextUI { get; private set; }
        [field : SerializeField] public TextUI PlayerMovementSpeedLevelUpgradeTextUI { get; private set; }
        [field : SerializeField] public TextUI PlayerMovementPriceNextSpeedLevelUpgradeTextUI { get; private set; }
    }
}