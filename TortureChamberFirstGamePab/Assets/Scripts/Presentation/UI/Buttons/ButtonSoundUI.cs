using UnityEngine;

namespace Scripts.Presentation.UI.Buttons
{
    public class ButtonSoundUI : ButtonUI
    {
        [SerializeField] private AudioSource _audioSource;

        protected override void OnAfterEnable() =>
            AddListener(OnClick);

        protected override void OnAfterDisable() =>
            RemoveListener(OnClick);

        private void OnClick() =>
            _audioSource.Play();
    }
}