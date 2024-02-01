﻿using System;
using System.Threading;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Visitors;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly VisitorInventory _visitorInventory;
        private readonly ProductShuffleService _productShuffleService;
        private readonly TavernMood _tavernMood;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly IVisitorView _visitorView;
        private readonly IVisitorImageUI _visitorImageUI;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorWaitingForOrderState
        (
            Visitor visitor,
            VisitorInventory visitorInventory,
            IVisitorImageUI visitorImageUI,
            ProductShuffleService productShuffleService,
            TavernMood tavernMood,
            IVisitorAnimation visitorAnimation,
            IVisitorView visitorView
        )
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
            _productShuffleService = productShuffleService ??
                                     throw new ArgumentNullException(nameof(productShuffleService));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _visitorAnimation = visitorAnimation ?? throw new ArgumentNullException(nameof(visitorAnimation));
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitorImageUI = visitorImageUI ??
                              throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            IItem item = _productShuffleService.GetRandomItem();

            _visitorImageUI.OrderImage.SetSprite(item.Icon);
            _visitorImageUI.OrderImage.ShowImage();
            _visitorImageUI.BackGroundImage.ShowImage();
            _visitorImageUI.BackGroundImage.SetFillAmount(Constant.FillingAmount.Maximum);

            _visitorInventory.SetTargetItem(item);
            _visitorAnimation.PlaySeatIdle();
            
            WaitAsync();
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

        //TODO плохо делать вилку за счет трай кетча
        private async void WaitAsync()
        {
            try
            {
                _visitorImageUI.BackGroundImage.ShowImage();
                await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(
                    Constant.Visitors.WaitingEatFillingRate, _cancellationTokenSource.Token,
                    () =>
                    {
                        //TODO вот таакие костыли
                        // _visitorAnimation.PlayIdle();
                        _visitorView.StopMove();
                        _visitorAnimation.PlaySeatIdle();
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
    }
}