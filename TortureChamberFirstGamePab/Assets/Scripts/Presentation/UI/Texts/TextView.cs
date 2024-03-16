﻿using TMPro;
using UnityEngine;

namespace Scripts.Presentation.UI.Texts
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public virtual void SetText(string text) =>
            _text.text = text;

        public void Enable() =>
            _text.enabled = true;

        public void Disable() =>
            _text.enabled = false;
    }
}