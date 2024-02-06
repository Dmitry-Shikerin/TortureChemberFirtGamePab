using System;
using Sources.Controllers.UI.AudioSources.Common;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources
{
    [RequireComponent(typeof(AudioSourceView))]
    public class AudioSourceUI : PresentableView<AudioSourceUIPresenter>, IAudioSourceUI
    {
        public AudioSourceView AudioSourceView { get; private set; }

        private void Awake() => 
            AudioSourceView = GetComponent<AudioSourceView>();

        //TODO насколько я понимаю обобщаем то што можно обобщить а для остального делаем частные случаи
        //TODO какието модели обобщаем, а для какихто делаем частные случаи
    }
}