using System;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Voids
{
    public class HUD : View
    {
        [field: SerializeField] public HudTextUIContainer TextUIContainer { get; private set; }

        private void OnValidate()
        {
            if (TextUIContainer == null)
                throw new NullReferenceException(nameof(TextUIContainer));
        }
    }
}
