using System;
using System.Threading;
using Sources.Domain.Constants;
using Sources.Domain.Items.Garbages;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Controllers.Items
{
    public class GarbagePresenter : PresenterBase
    {
        private readonly Garbage _garbage;
        private readonly IGarbageView _garbageView;
        private readonly PickUpPointUIImages _pickUpPointUIImages;

        private CancellationTokenSource _cancellationTokenSource;

        public GarbagePresenter(
            PickUpPointUIImages pickUpPointUIImages,
            IGarbageView garbageView,
            Garbage garbage)
        {
            _pickUpPointUIImages = pickUpPointUIImages
                ? pickUpPointUIImages
                : throw new ArgumentNullException(nameof(pickUpPointUIImages));
            _garbageView = garbageView ?? throw new ArgumentNullException(nameof(garbageView));
            _garbage = garbage ?? throw new ArgumentNullException(nameof(garbage));
        }

        public IEatPointView EatPointView => _garbage.EatPointView;

        public override void Enable()
        {
            _pickUpPointUIImages.BackgroundImage.SetFillAmount(Constant.FillingAmount.Maximum);
        }

        public async void CleanUpAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                _garbage.StartAudioSource();

                await _pickUpPointUIImages.BackgroundImage.FillMoveTowardsAsync(
                    _garbageView.FillingRate,
                    _cancellationTokenSource.Token);

                _garbage.StopAudioSource();

                _pickUpPointUIImages.BackgroundImage.SetFillAmount(Constant.FillingAmount.Maximum);
                _garbageView.Destroy();
                _garbage.EatPointView.Clean();
            }
            catch (OperationCanceledException)
            {
                _garbage.StopAudioSource();

                _pickUpPointUIImages.BackgroundImage.SetFillAmount(Constant.FillingAmount.Maximum);
            }
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public void SetEatPointView(IEatPointView eatPointView)
        {
            _garbage.SetEatPointView(eatPointView);
        }
    }
}