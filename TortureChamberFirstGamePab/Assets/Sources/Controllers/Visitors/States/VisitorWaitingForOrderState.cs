using System;
using System.Threading;
using JetBrains.Annotations;
using Sources.Domain.Items;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemRepository<IItem> _itemRepository;
        private readonly ProductShuffleService _productShuffleService;
        private readonly TavernMood _tavernMood;
        private readonly IVisitorImageUI _visitorImageUI;

        // private bool _isCanceled;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorWaitingForOrderState(Visitor visitor,
            VisitorInventory visitorInventory,
            IVisitorImageUI visitorImageUI,
            ItemRepository<IItem> itemRepository, ProductShuffleService productShuffleService,
            TavernMood tavernMood)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
            _itemRepository = itemRepository ??
                              throw new ArgumentNullException(nameof(itemRepository));
            _productShuffleService = productShuffleService ??
                                     throw new ArgumentNullException(nameof(productShuffleService));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _visitorImageUI = visitorImageUI ??
                              throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource = new CancellationTokenSource();
            // Debug.Log("Посетитель в состоянии ожидания заказа");
            // Beer beer = _itemRepository.Get<Beer>();
            IItem item = _productShuffleService.GetRandomItem();

            _visitorImageUI.OrderImage.SetSprite(item.Icon);
            _visitorImageUI.OrderImage.ShowImage();
            _visitorImageUI.BackGroundImage.ShowImage();

            _visitorInventory.SetTargetItem(item);

            Wait();
        }

        public override void Update()
        {
            if (_visitorInventory.Item != null)
            {
                _cancellationTokenSource.Cancel();
                //TODO не забыть поправить булку
                // _isCanceled = true;
            }
        }

        public override void Exit()
        {
            _visitorInventory.SetTargetItem(null);
        }

        private async void Wait()
        {
            try
            {
                await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(
                    0.02f, _cancellationTokenSource.Token);
                _visitor.SetUnHappy(true);
                _tavernMood.RemoveTavernMood();
            }
            catch (OperationCanceledException e)
            {
                //TODO чтото обработать
                Debug.Log("получил пролдукт");
                _visitorInventory.SetTargetItem(null);
                _visitor.SetCanEat(true);
            }
        }
    }
}