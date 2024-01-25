using System;
using System.Threading;
using Sources.Domain.Constants;
using Sources.Domain.Items.Garbages;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Garbages;

namespace Sources.Controllers.Items
{
    public class GarbagePresenter : PresenterBase
    {
        private readonly PickUpPointUIImages _pickUpPointUIImages;
        private readonly IGarbageView _garbageView;
        private readonly Garbage _garbage;

        private CancellationTokenSource _cancellationTokenSource;

        public GarbagePresenter
        (
            PickUpPointUIImages pickUpPointUIImages,
            IGarbageView garbageView,
            Garbage garbage
        )
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
                await _pickUpPointUIImages.BackgroundImage.FillMoveTowardsAsync(
                    _garbageView.FillingRate, _cancellationTokenSource.Token);
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(1);
                _garbageView.Destroy();
                _garbage.EatPointView.Clean();
            }
            catch (OperationCanceledException)
            {
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(1);
            }
        }

        public void Cancel() =>
            _cancellationTokenSource.Cancel();

        public void SetEatPointView(IEatPointView eatPointView) =>
            _garbage.SetEatPointView(eatPointView);
    }
}