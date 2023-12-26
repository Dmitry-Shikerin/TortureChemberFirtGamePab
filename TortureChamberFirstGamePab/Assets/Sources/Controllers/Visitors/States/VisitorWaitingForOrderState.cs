using System;
using System.Threading;
using JetBrains.Annotations;
using Sources.Domain.Items;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemRepository<IItem> _itemRepository;
        private readonly ProductShuffleService _productShuffleService;
        private readonly IVisitorImageUI _visitorImageUI;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorWaitingForOrderState(Visitor visitor,
            VisitorInventory visitorInventory,
            IVisitorImageUI visitorImageUI,
            ItemRepository<IItem> itemRepository, ProductShuffleService productShuffleService)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
            _itemRepository = itemRepository ??
                              throw new ArgumentNullException(nameof(itemRepository));
            _productShuffleService = productShuffleService ?? 
                                     throw new ArgumentNullException(nameof(productShuffleService));
            _visitorImageUI = visitorImageUI ??
                              throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                // Debug.Log("Посетитель в состоянии ожидания заказа");
                // Beer beer = _itemRepository.Get<Beer>();
                IItem item = _productShuffleService.GetRandomItem();

                _visitorImageUI.OrderImage.SetSprite(item.Icon);
                _visitorImageUI.OrderImage.Show();
                _visitorImageUI.BackGroundImage.Show();

                _visitorInventory.SetTargetItem(item);

                Wait();
            }
            catch (OperationCanceledException)
            {
                //TODO чтото обработать
            }
        }

        public override void Update()
        {
            if (_visitorInventory.Item != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        public override void Exit()
        {
            _visitorInventory.SetTargetItem(null);
        }

        private async void Wait()
        {
            await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(0.05f, _cancellationTokenSource.Token);
            _visitor.SetUnHappy(true);
        }
    }
}