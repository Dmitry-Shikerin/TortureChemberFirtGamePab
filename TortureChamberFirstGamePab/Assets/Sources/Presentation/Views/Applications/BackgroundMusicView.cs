using System;
using UnityEngine;

namespace Sources.Presentation.Views.Applications
{
    public class BackgroundMusicView : View
    {
        [SerializeField] private AudioSource _audioSource;

        //TODO возможно переделать на энтер и эксит и вызывать их в стейте
        private void OnEnable()
        {
            _audioSource.Play();
        }

        private void OnDisable()
        {
            _audioSource.Stop();
        }
    }
}