using System;
using UnityEngine;

namespace Sources.Presentation.UI.Conteiners
{
    //TODO доделать
    public class ButtonSoundUI : ButtonUI
    {
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            AddListener(OnClick);
        }

        private void OnClick()
        {
            _audioSource.Play();
        }
        
        //TODO сделать сервис для музыки он будет монобехом
    }
}