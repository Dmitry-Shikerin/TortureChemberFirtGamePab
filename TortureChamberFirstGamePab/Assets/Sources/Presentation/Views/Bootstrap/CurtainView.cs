using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Exceptions.Serializefields;
using UnityEngine;

namespace Sources.Presentation.Views.Bootstrap
{
    public class CurtainView : MonoBehaviour
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

        public async UniTask Show() => 
            await Fade(0, 1);

        public async UniTask Hide() => 
            await Fade(1, 0);

        private async UniTask Fade(float startAlpha, float endAlpha)
        {
            _canvasGroup.alpha = startAlpha;

            while (Mathf.Abs(_canvasGroup.alpha - endAlpha) > 0.01)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(
                    _canvasGroup.alpha, endAlpha, Time.deltaTime / _duration);

                await UniTask.Yield();
            }

            _canvasGroup.alpha = endAlpha;
        }
    }
}