using UnityEngine;

namespace Sources.Presentation.UI.Conteiners.MainMenu
{
    public class HudButtonUIContainer : MonoBehaviour
    {
        [field: SerializeField] public ButtonUI ContinueGameButton { get; private set; }
        [field: SerializeField] public ButtonUI NewGameButton { get; private set; }
        [field: SerializeField] public ButtonUI LeaderboardButton { get; private set; }
        [field: SerializeField] public ButtonUI BackToMainMenuButton { get; private set; }
        //TODO в какую папку положить контейнеры?
    }
}