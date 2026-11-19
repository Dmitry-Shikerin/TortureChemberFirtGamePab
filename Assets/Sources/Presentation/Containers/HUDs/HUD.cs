using Lean.Localization;
using Presentation.Containers.Forms;
using Presentation.Containers.UI;
using Presentation.Containers.UI.AudioSources;
using Presentation.Containers.UI.Buttons;
using Presentation.Containers.UI.Texts;
using Presentation.UI;
using Presentation.UI.AudioSources.BackgroundMusics;
using Presentation.UI.Buttons;
using Presentation.Views;
using Presentation.Views.Player.Inventory;
using Presentation.Views.Taverns;
using Presentation.Views.Taverns.UpgradePoints;
using UnityEngine;

namespace Presentation.Containers.HUDs
{
    public class HUD : View
    {
        [field: SerializeField] public HudTextUIContainer TextUIContainer { get; private set; }
        [field: SerializeField] public PlayerInventorySlotsImages PlayerInventorySlotsImages { get; private set; }
        [field: SerializeField] public TavernMoodView TavernMoodView { get; private set; }
        [field: SerializeField] public TavernUpgradePointView TavernUpgradePointView { get; private set; }
        [field: SerializeField] public TavernUpgradePointButtons TavernUpgradePointButtons { get; private set; }
        [field: SerializeField] public PlayerUpgradeViewsContainer PlayerUpgradeViewsContainer { get; private set; }
        [field: SerializeField] public PauseMenuButtonContainer PauseMenuButtonContainer { get; private set; }
        [field: SerializeField] public ImageUI TavernMoodImageUI { get; private set; }
        [field: SerializeField] public ButtonUI PauseMenuButton { get; private set; }
        [field: SerializeField] public LeanLocalization LeanLocalization { get; private set; }
        [field: SerializeField] public GameplayFormsContainer GameplayFormsContainer { get; private set; }
        [field: SerializeField] public ContainerView ContainerView { get; private set; }
        [field: SerializeField] public CongratulationUpgradeAudioSourceContainer
            CongratulationUpgradeAudioSourceContainer { get; private set; }
        [field: SerializeField] public TutorialFormButtonContainer TutorialFormButtonContainer { get; private set; }
        [field: SerializeField] public LoadFormButtonContainer LoadFormButtonContainer { get; private set; }
        [field: SerializeField] public GameOverFormButtonContainer GameOverFormButtonContainer { get; private set; }
        [field: SerializeField] public AdvertisingAfterCertainPeriodViewContainer
            AdvertisingAfterCertainPeriodViewContainer { get; private set; }
        [field: SerializeField] public SettingFormButtonContainer
            SettingFormButtonContainer { get; private set; }
        [field: SerializeField] public JoysticksContainer JoysticksContainer { get; private set; }
        [field: SerializeField] public GameOverTextContainer GameOverTextContainer { get; private set; }
        [field: SerializeField] public BackgroundMusicView BackgroundMusicView { get; private set; }
    }
}