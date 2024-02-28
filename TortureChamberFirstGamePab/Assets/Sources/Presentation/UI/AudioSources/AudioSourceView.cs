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
            Debug.Log("Set loop");
            _audioSource.loop = true;
        }

        public void RemoveLoop()
        {
            Debug.Log("Remove loop");
            _audioSource.loop = false;
        }

        public void Pause()
        {
            Debug.Log("Pause");
            _audioSource.Pause();
        }

        public void Continue()
        {
            Debug.Log("Continue");
            _audioSource.UnPause();
        }

        public void SetVolume(float value)
        {
            _audioSource.volume = value;
        }
    }
}