using Sources.Domain.Constants;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources
{
    //TODO как сделать воспроизведение звуков в остальных контроллерах?
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

        public void SetVolume(float value)
        {
            value = Mathf.Clamp(value, Constant.Volume.Min, Constant.Volume.Min);
            
            _audioSource.volume = value;
        }
    }
}