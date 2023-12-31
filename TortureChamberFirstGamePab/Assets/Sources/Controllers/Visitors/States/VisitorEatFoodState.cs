﻿using System;
using System.Threading;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Garbages;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorEatFoodState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly TavernMood _tavernMood;
        private readonly GarbageBuilder _garbageBuilder;
        private readonly CoinBuilder _coinBuilder;
        private readonly VisitorImageUI _visitorImageUI;

        private const float FillingRate = 0.2f;

        public VisitorEatFoodState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation, CollectionRepository collectionRepository,
            VisitorInventory visitorInventory, VisitorImageUI visitorImageUI,
            ItemViewFactory itemViewFactory, TavernMood tavernMood,
            GarbageBuilder garbageBuilder, CoinBuilder coinBuilder)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorInventory = visitorInventory ?? throw new ArgumentNullException(nameof(visitorInventory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _garbageBuilder = garbageBuilder ?? throw new ArgumentNullException(nameof(garbageBuilder));
            _coinBuilder = coinBuilder ?? throw new ArgumentNullException(nameof(coinBuilder));
            _visitorImageUI = visitorImageUI ? visitorImageUI : 
                throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            IItemView itemView = _itemViewFactory.Create(_visitorInventory.Item);
            itemView.SetPosition(_visitor.SeatPointView.EatPointView.transform);
            Debug.Log(itemView);
            Eat(itemView);
        }

        public override void Exit()
        {
            _visitor.SeatPointView.UnOccupy();
            _visitor.FinishEating();
            IGarbageView garbageView = _garbageBuilder.Create();
            garbageView.SetPosition(_visitor.SeatPointView.EatPointView.Position);
            garbageView.SetEatPointView(_visitor.SeatPointView.EatPointView);
            _visitor.SeatPointView.EatPointView.GetDirty();
            ICoinAnimationView coinAnimationView = _coinBuilder.Create();
            coinAnimationView.SetTransformPosition(_visitor.SeatPointView.EatPointView.Position);
            coinAnimationView.SetCoinAmount(_visitorInventory.Item.Price);
        }

        private async void Eat(IItemView itemView)
        {
            await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(
                FillingRate, new CancellationTokenSource().Token);
            _visitorImageUI.BackGroundImage.SetFillAmount(Constant.MaximumAmountFillingImage);
            _visitor.SetUnSeat();
            itemView.Destroy();
            _tavernMood.AddTavernMood();
        }
    }
}