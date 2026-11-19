using Presentation.UI.Texts;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.Texts
{
    public class GameOverTextContainer : View
    {
        [field: SerializeField] public TextUI ScoreText { get; private set; }
    }
}