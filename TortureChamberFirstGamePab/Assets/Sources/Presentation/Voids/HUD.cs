using System;
using Lean.Localization;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Forms.Gameplays.Containers;
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
        [field: SerializeField] public PauseMenuWindow PauseMenuWindow { get; private set; }
        [field: SerializeField] public ImageUI TavernMoodImageUI { get; private set; }
        [field: SerializeField] public ButtonUI PauseMenuButton { get; private set; }
        [field: SerializeField] public LeanLocalization LeanLocalization { get; private set; }
        [field: SerializeField] public GameplayFormsContainer GameplayFormsContainer { get; private set; }
        [field: SerializeField] public ContainerView ContainerView { get; private set; }
    }
}
