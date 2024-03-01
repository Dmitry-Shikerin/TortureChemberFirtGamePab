using System;
using System.Threading;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Extensions.ShuffleExtensions;
using Sources.Utils.Repositoryes.ItemRepository.Interfaces;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly VisitorInventory _visitorInventory;
        private readonly TavernMood _tavernMood;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly IVisitorView _visitorView;
        private readonly IItemProvider<IItem> _itemProvider;
        private readonly IVisitorImageUI _visitorImageUI;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorWaitingForOrderState
        (
            Visitor visitor,
            VisitorInventory visitorInventory,
            IVisitorImageUI visitorImageUI,
            TavernMood tavernMood,
            IVisitorAnimation visitorAnimation,
            IVisitorView visitorView,
            IItemProvider<IItem> itemProvider)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _visitorAnimation = visitorAnimation ?? throw new ArgumentNullException(nameof(visitorAnimation));
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
            _visitorImageUI = visitorImageUI ??
                              throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            IItem item = _itemProvider.Collection.GetRandomItem();

            _visitorImageUI.OrderImage.SetSprite(item.Icon);
            _visitorImageUI.OrderImage.ShowImage();
            _visitorImageUI.BackGroundImage.ShowImage();
            _visitorImageUI.BackGroundImage.SetFillAmount(Constant.FillingAmount.Maximum);

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

                //TODO спросить про рекламу и продвижение
                //TODo спросить про стикки баннер
                //TODO увеличить стоимость улучшений
                //TODO немного увеличить радиус сбора мусора
                //TODO потом раскоментировать
                await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(
                    Constant.Visitors.WaitingEatFillingRate, cancellationToken, () =>
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
                _visitor.Eat();
            }
        }

        private void Cancel() => 
            _cancellationTokenSource.Cancel();
    }
}