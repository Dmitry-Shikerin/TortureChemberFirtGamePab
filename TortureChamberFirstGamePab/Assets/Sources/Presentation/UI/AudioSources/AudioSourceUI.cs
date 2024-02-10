using System;
using Sources.Controllers.UI.AudioSources;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources
{
    public class AudioSourceUI : PresentableView<AudioSourceUIPresenterBase>, IAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView AudioSourceView { get; private set; }
        
        //TODO насколько я понимаю обобщаем то што можно обобщить а для остального делаем частные случаи
        //TODO какието модели обобщаем, а для какихто делаем частные случаи
    }
}