using System;
using System.Threading;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Items.Garbages;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Garbages;

namespace Sources.Controllers.Items
{
    public class GarbagePresenter : PresenterBase
    {
        private readonly PickUpPointUI _pickUpPointUI;
        private readonly IGarbageView _garbageView;
        private readonly Garbage _garbage;

        private CancellationTokenSource _cancellationTokenSource;

        public GarbagePresenter(PickUpPointUI pickUpPointUI, IGarbageView garbageView, Garbage garbage)
        {
            _pickUpPointUI = pickUpPointUI ?
                pickUpPointUI : throw new ArgumentNullException(nameof(pickUpPointUI));
            _garbageView = garbageView ?? throw new ArgumentNullException(nameof(garbageView));
            _garbage = garbage ?? throw new ArgumentNullException(nameof(garbage));
        }

        public IEatPointView EatPointView => _garbage.EatPointView;

        public async void CleanUp()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                await _pickUpPointUI.BackgroundImage.FillMoveTowardsAsync(
                    _garbageView.FillingRate, _cancellationTokenSource.Token);
                _garbageView.Destroy();
                _garbage.EatPointView.Clean();
            }
            catch (OperationCanceledException exception)
            {
                _pickUpPointUI.BackgroundImage.SetFillAmount(1);
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