using UnityEngine;

namespace Sources.Presentation.Views.Forms.Gameplays.Containers
{
    public class GameplayFormsContainer : View
    {
        [field: SerializeField] public HudFormView HudFormView { get; private set; }
        [field: SerializeField] public PauseMenuFormView PauseMenuFormView { get; private set; }
        [field: SerializeField] public UpgradeFormView UpgradeFormView { get; private set; }
        [field: SerializeField] public TutorialFormView TutorialFormView { get; private set; }
    }
}