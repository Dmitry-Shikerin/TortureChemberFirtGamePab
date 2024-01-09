using System;
using System.Threading;
using Sources.Domain.Constants;
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
        private const float FillingRate = 0.02f; 
        
        private readonly Visitor _visitor;
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemRepository<IItem> _itemRepository;
        private readonly ProductShuffleService _productShuffleService;
        private readonly TavernMood _tavernMood;
        private readonly IVisitorImageUI _visitorImageUI;

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
            
            IItem item = _productShuffleService.GetRandomItem();

            // Debug.Log($"Посетитель заказал {item}");
            _visitorImageUI.OrderImage.SetSprite(item.Icon);
            _visitorImageUI.OrderImage.ShowImage();
            _visitorImageUI.BackGroundImage.ShowImage();
            _visitorImageUI.BackGroundImage.SetFillAmount(Constant.MaximumAmountFillingImage);

            _visitorInventory.SetTargetItem(item);

            Wait();
        }

        public override void Update()
        {
            if (_visitorInventory.Item != null) 
                _cancellationTokenSource.Cancel();
        }

        public override void Exit()
        {
            _visitorInventory.SetTargetItem(null);
        }

        private async void Wait()
        {
            try
            {
                _visitorImageUI.BackGroundImage.ShowImage();
                await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(
                    FillingRate, _cancellationTokenSource.Token);
                _visitor.SetUnHappy();
                _tavernMood.RemoveTavernMood();
                _visitor.SeatPointView.UnOccupy();
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("получил пролдукт");
                _visitorInventory.SetTargetItem(null);
                _visitor.Eat();
            }
        }
    }
}