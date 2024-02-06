using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.Exceptions.Serializefields;
using UnityEngine;

namespace Sources.Presentation.Views.Bootstrap
{
    public class CurtainView : View
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 1;

        private void OnValidate()
        {
            if (_canvasGroup == null)
                throw new NullReferenceException(nameof(CanvasGroup));

            if (_duration != 1f)
                throw new SerializeFieldNumberChangedException("Поле изменено", nameof(_duration));
        }

        private void Awake()
        {
            if (_canvasGroup == null)
                throw new NullReferenceException(nameof(_canvasGroup));
            
            DontDestroyOnLoad(this);
            _canvasGroup.alpha = 0;
        }

        public async UniTask ShowCurtain()
        {
            Show();
            await Fade(Constant.FillingAmount.Minimum, Constant.FillingAmount.Maximum);
        }

        public async UniTask HideCurtain()
        {
            await Fade(Constant.FillingAmount.Maximum, Constant.FillingAmount.Minimum);
            Hide();
        }

        private async UniTask Fade(float startAlpha, float endAlpha)
        {
            _canvasGroup.alpha = startAlpha;

            while (Mathf.Abs(_canvasGroup.alpha - endAlpha) > Constant.Epsilon)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(
                    _canvasGroup.alpha, endAlpha, Time.deltaTime / _duration);

                await UniTask.Yield();
            }

            _canvasGroup.alpha = endAlpha;
        }
    }
}