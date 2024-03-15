using System;
using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using UnityEngine;

namespace Scripts.Presentation.Views.Applications
{
    public class CurtainView : View
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 1;

        public bool IsInProgress { get; private set; }

        private void Awake()
        {
            if (_canvasGroup == null)
                throw new NullReferenceException(nameof(_canvasGroup));

            DontDestroyOnLoad(this);
            _canvasGroup.alpha = 0;
        }

        public async UniTask ShowCurtain()
        {
            IsInProgress = true;
            Show();
            await Fade(FillingAmountConstant.Minimum, FillingAmountConstant.Maximum);
        }

        public async UniTask HideCurtain()
        {
            await Fade(FillingAmountConstant.Maximum, FillingAmountConstant.Minimum);
            Hide();
            IsInProgress = false;
        }

        private async UniTask Fade(float startAlpha, float endAlpha)
        {
            _canvasGroup.alpha = startAlpha;

            while (Mathf.Abs(_canvasGroup.alpha - endAlpha) > MathfConstant.Epsilon)
            {
                _canvasGroup.alpha = Mathf.MoveTowards(
                    _canvasGroup.alpha,
                    endAlpha,
                    Time.deltaTime / _duration);

                await UniTask.Yield();
            }

            _canvasGroup.alpha = endAlpha;
        }
    }
}