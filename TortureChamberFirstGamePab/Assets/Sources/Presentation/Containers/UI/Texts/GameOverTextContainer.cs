using System;
using Sources.Presentation.UI;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Containers.UI.Texts
{
    public class GameOverTextContainer : View
    {
        [field: SerializeField] public TextUI ScoreText { get; private set; }
    }
}