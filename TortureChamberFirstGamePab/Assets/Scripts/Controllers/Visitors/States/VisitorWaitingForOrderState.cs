using System;
using System.Threading;
using Scripts.Domain.Constants;
using Scripts.Domain.Visitors;
using Scripts.DomainInterfaces.Items;
using Scripts.Infrastructure.StateMachines.FiniteStateMachines.States;
using Scripts.PresentationInterfaces.Animations;
using Scripts.PresentationInterfaces.Views.Visitors;
using Scripts.Utils.Extensions.ShuffleExtensions;
using Scripts.Utils.Repositories.ItemRepository.Interfaces;

namespace Scripts.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly IItemProvider<IItem> _itemProvider;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly IVisitorImageUI _visitorImageUI;
        private readonly VisitorInventory _visitorInventory;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorWaitingForOrderState(
            Visitor visitor,
            VisitorInventory visitorInventory,
            IVisitorImageUI visitorImageUI,
            IVisitorAnimation visitorAnimation,
            IItemProvider<IItem> itemProvider)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
            _visitorAnimation = visitorAnimation ?? throw new ArgumentNullException(nameof(visitorAnimation));
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
            _visitorImageUI = visitorImageUI ??
                              throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var item = _itemProvider.Collection.GetRandomItem();

            _visitorImageUI.OrderImage.SetSprite(item.Icon);
            _visitorImageUI.OrderImage.ShowImage();
            _visitorImageUI.BackGroundImage.ShowImage();
            _visitorImageUI.BackGroundImage.SetFillAmount(FillingAmountConstant.Maximum);

            _visitorInventory.SetTargetItem(item);
            _visitorAnimation.PlaySeatIdle();

            WaitAsync(_cancellationTokenSource.Token);
        }

        public override void Exit()
        {
            _visitorInventory.SetTargetItem(null);

            _cancellationTokenSource.Cancel();
        }

        private async void WaitAsync(CancellationToken cancellationToken)
        {
            try
            {
                _visitorImageUI.BackGroundImage.ShowImage();

                await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(
                    VisitorConstant.WaitingEatFillingRate, cancellationToken, () =>
                    {
                        if (_visitorInventory.Item != null)
                            Cancel();
                    });

                _visitor.SetUnHappy();
                _visitor.SeatPointView.UnOccupy();
            }
            catch (OperationCanceledException)
            {
                _visitorInventory.SetTargetItem(null);
            }
        }

        private void Cancel() =>
            _cancellationTokenSource.Cancel();
    }
}