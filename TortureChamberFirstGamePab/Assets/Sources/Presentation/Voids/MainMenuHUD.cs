using Lean.Localization;
using Sources.Presentation.UI.Conteiners.MainMenu;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Forms.MainMenus.Containers;
using Sources.Presentation.Views.YandexSDC.MyVariant;
using UnityEngine;

public class MainMenuHUD : View
{
    [field: SerializeField] public HudButtonUIContainer ButtonUIContainer { get; private set; }
    [field: SerializeField] public ContainerView ContainerView { get; private set; }
    [field: SerializeField] public LeaderboardElementViewContainer 
        LeaderboardElementViewContainer { get; private set; }
    [field: SerializeField] public LeanLocalization LeanLocalization { get; private set; }
    [field: SerializeField] public MainMenuFormsContainer MainMenuFormsContainer { get; private set; }
}
