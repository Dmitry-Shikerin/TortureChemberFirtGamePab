﻿using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.Exceptions.Serializefields;
using UnityEngine;

namespace Sources.Presentation.Views.Applications
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

        private void OnValidate()
        {
            if (_canvasGroup == null)
                throw new NullReferenceException(nameof(CanvasGroup));

            if (_duration != 1f)
                throw new SerializeFieldNumberChangedException("Поле изменено", nameof(_duration));
        }

        public event Action CurtainHide;

        public async UniTask ShowCurtain()
        {
            IsInProgress = true;
            Show();
            await Fade(Constant.FillingAmount.Minimum, Constant.FillingAmount.Maximum);
        }

        public async UniTask HideCurtain()
        {
            await Fade(Constant.FillingAmount.Maximum, Constant.FillingAmount.Minimum);
            Hide();
            IsInProgress = false;
        }

        private async UniTask Fade(float startAlpha, float endAlpha)
        {
            _canvasGroup.alpha = startAlpha;

            while (Mathf.Abs(_canvasGroup.alpha - endAlpha) > Constant.Epsilon)
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