using Scripts.Presentation.Views;
using Scripts.Presentation.Views.Forms;
using Scripts.Presentation.Views.Forms.Gameplays;
using UnityEngine;

namespace Scripts.Presentation.Containers.Forms
{
    public class GameplayFormsContainer : View
    {
        [field: SerializeField] public HudFormView HudFormView { get; private set; }
        [field: SerializeField] public PauseMenuFormView PauseMenuFormView { get; private set; }
        [field: SerializeField] public UpgradeFormView UpgradeFormView { get; private set; }
        [field: SerializeField] public TutorialFormView TutorialFormView { get; private set; }
        [field: SerializeField] public LoadFormView LoadFormView { get; private set; }
        [field: SerializeField] public GameOverFormView GameOverFormView { get; private set; }
        [field: SerializeField] public SettingFormView SettingFormView { get; private set; }
    }
}