using System;
using UnityEngine;

namespace Sources.Presentation.Views.Applications
{
    public class BackgroundMusicView : View
    {
        [SerializeField] private AudioSource _audioSource;

        //TODO возможно переделать на энтер и эксит и вызывать их в стейте
        //TODO как поступить с этим сервисом?
        //TODO перекидывать его со сцены на сцену?
        private void OnEnable() => 
            _audioSource.Play();

        private void OnDisable() => 
            _audioSource.Stop();
    }
}