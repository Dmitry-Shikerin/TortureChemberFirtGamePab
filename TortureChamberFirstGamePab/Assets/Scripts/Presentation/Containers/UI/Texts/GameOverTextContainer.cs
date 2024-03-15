using Scripts.Presentation.UI.Texts;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Texts
{
    public class GameOverTextContainer : View
    {
        [field: SerializeField] public TextUI ScoreText { get; private set; }
    }
}