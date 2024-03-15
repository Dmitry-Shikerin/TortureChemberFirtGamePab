using Lean.Localization;
using Scripts.Presentation.Containers.Forms;
using Scripts.Presentation.Containers.UI.Buttons;
using Scripts.Presentation.UI.AudioSources.BackgroundMusics;
using Scripts.Presentation.Views;
using Scripts.Presentation.Views.YandexSDC.MyVariant;
using UnityEngine;

namespace Scripts.Presentation.Containers.HUDs
{
    public class MainMenuHUD : View
    {
        [field: SerializeField] public HudButtonUIContainer ButtonUIContainer { get; private set; }
        [field: SerializeField] public ContainerView ContainerView { get; private set; }

        [field: SerializeField] public LeaderboardElementViewContainer
            LeaderboardElementViewContainer { get; private set; }
        [field: SerializeField] public LeanLocalization LeanLocalization { get; private set; }
        [field: SerializeField] public MainMenuFormsContainer MainMenuFormsContainer { get; private set; }
        [field: SerializeField] public BackgroundMusicView BackgroundMusicView { get; private set; }
    }
}