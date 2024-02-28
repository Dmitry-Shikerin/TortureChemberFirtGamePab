using Sources.Domain.Constants;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources
{
    public class AudioSourceView : View
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play() => 
            _audioSource.Play();

        public void Stop() => 
            _audioSource.Stop();

        public void SetLoop()
        {
            _audioSource.loop = true;
        }

        public void RemoveLoop()
        {
            _audioSource.loop = false;
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void Continue()
        {
            _audioSource.UnPause();
        }

        public void Mute() => 
            _audioSource.mute = true;

        public void UnMute() => 
            _audioSource.mute = false;

        public void SetVolume(float value)
        {
            _audioSource.volume = value;
        }
    }
}