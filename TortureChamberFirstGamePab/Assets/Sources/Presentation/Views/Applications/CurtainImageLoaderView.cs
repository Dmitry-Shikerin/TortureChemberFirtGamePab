using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Views.Applications
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

        public void StopTwist()
        {
            _isSpinning = false;
        }

        private async UniTask Twist()
        {
            while (_isSpinning)
            {
                _image.rectTransform.Rotate(0, 0 ,
                    Constant.ImageRotate.Speed);
                
                await UniTask.Yield();
            }
        }
    }
}