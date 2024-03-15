using UnityEngine;

namespace Scripts.Presentation.UI.Buttons
{
    public class ButtonSoundView : ButtonView
    {
        [SerializeField] private AudioSource _audioSource;

        protected void OnEnable() =>
            AddClickListener(OnClick);

        protected void OnDisable() =>
            RemoveClickListener(OnClick);

        private void OnClick() =>
            _audioSource.Play();
    }
}