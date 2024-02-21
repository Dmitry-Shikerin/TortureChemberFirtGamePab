using System;
using Lean.Localization;
using Sources.Presentation.Containers.Forms;
using Sources.Presentation.Containers.UI.Buttons;
using Sources.Presentation.Containers.UI.Texts;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.UI.Conteiners.AudioSources;
using Sources.Presentation.UI.Conteiners.Buttons;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Views.UIs;
using UnityEngine;

namespace Sources.Presentation.Voids
{
    public class HUD : View
    {
        [field: SerializeField] public HudTextUIContainer TextUIContainer { get; private set; }
        [field: SerializeField] public PlayerInventorySlotsImages PlayerInventorySlotsImages { get; private set; }
        [field: SerializeField] public TavernMoodView TavernMoodView { get; private set; }
        [field: SerializeField] public TavernUpgradePointView TavernUpgradePointView { get; private set; }
        [field: SerializeField] public TavernUpgradePointButtons TavernUpgradePointButtons { get; private set; }
        [field: SerializeField] public TavernUpgradePointTextUIs TavernUpgradePointButtonsTextUIs { get; private set; }
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
        [field: SerializeField] public AdvertisingAfterCertainPeriodTextContainer
            AdvertisingAfterCertainPeriodTextContainer { get; private set; }
        [field: SerializeField] public SettingFormButtonContainer
            SettingFormButtonContainer { get; private set; }
    }
}
