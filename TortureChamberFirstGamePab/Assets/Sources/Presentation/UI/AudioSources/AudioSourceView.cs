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

        public void SetLoop() => 
            _audioSource.loop = true;

        public void RemoveLoop() => 
            _audioSource.loop = false;

        public void Pause() => 
            _audioSource.Pause();

        public void UnPause() => 
            _audioSource.UnPause();

        //TODO сделать уравление звуком для аудиосоурсов кнопок
        public void SetVolume(float value)
        {
            _audioSource.volume = value;
        }
    }
}