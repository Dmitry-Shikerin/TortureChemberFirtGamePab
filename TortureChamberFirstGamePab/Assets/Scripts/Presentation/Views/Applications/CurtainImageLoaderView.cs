using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Presentation.Views.Applications
{
    public class CurtainImageLoaderView : View
    {
        [SerializeField] private Image _image;

        private bool _isSpinning;

        public async void PlayTwist()
        {
            _isSpinning = true;

            await Twist();
        }

        public void StopTwist() =>
            _isSpinning = false;

        private async UniTask Twist()
        {
            while (_isSpinning)
            {
                _image.rectTransform.Rotate(0, 0, ImageRotateConstant.Speed);

                await UniTask.Yield();
            }
        }
    }
}