using System;
using System.Threading;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Items.Garbages;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Garbages;

namespace Sources.Controllers.Items
{
    public class GarbagePresenter : PresenterBase
    {
        private readonly PickUpPointUI _pickUpPointUI;
        private readonly IGarbageView _garbageView;
        private readonly Garbage _garbage;

        public GarbagePresenter(PickUpPointUI pickUpPointUI, IGarbageView garbageView, Garbage garbage)
        {
            _pickUpPointUI = pickUpPointUI ?
                pickUpPointUI : throw new ArgumentNullException(nameof(pickUpPointUI));
            _garbageView = garbageView ?? throw new ArgumentNullException(nameof(garbageView));
            _garbage = garbage ?? throw new ArgumentNullException(nameof(garbage));
        }

        public async void CleanUp(CancellationToken cancellationToken)
        {
            try
            {
                await _pickUpPointUI.BackgroundImage.FillMoveTowardsAsync(_garbageView.FillingRate, cancellationToken);
                _garbageView.Destroy();
            }
            catch (OperationCanceledException exception)
            {
                //TODO чтото обработать
                Console.WriteLine(exception);
                throw;
            }
            
        }
    }
}