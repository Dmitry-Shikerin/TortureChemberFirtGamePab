using System;
using Sources.Controllers.Visitors;
using Sources.Controllers.Visitors.States;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.Transitions;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Controllers.Visitors
{
    public class VisitorPresenterFactory
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly ProductShuffleService _productShuffleService;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly GarbageSpawner _garbageSpawner;
        private readonly CoinSpawner _coinSpawner;

        public VisitorPresenterFactory
        (
            CollectionRepository collectionRepository,
            ProductShuffleService productShuffleService,
            ImageUIFactory imageUIFactory,
            ItemViewFactory itemViewFactory,
            GarbageSpawner garbageSpawner,
            CoinSpawner coinSpawner
        )
        {
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _productShuffleService =
                productShuffleService ?? throw new ArgumentNullException(nameof(productShuffleService));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _garbageSpawner = garbageSpawner ?? throw new ArgumentNullException(nameof(garbageSpawner));
            _coinSpawner = coinSpawner ?? throw new ArgumentNullException(nameof(coinSpawner));
        }

        public VisitorPresenter Create(IVisitorView visitorView,
            IVisitorAnimation visitorAnimation, Visitor visitor,
            VisitorInventory visitorInventory, TavernMood tavernMood
            , VisitorCounter visitorCounter)
        {
            if (visitorView == null)
                throw new ArgumentNullException(nameof(visitorView));
            if (visitorAnimation == null)
                throw new ArgumentNullException(nameof(visitorAnimation));
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));
            if (visitorInventory == null)
                throw new ArgumentNullException(nameof(visitorInventory));
            if (tavernMood == null)
                throw new ArgumentNullException(nameof(tavernMood));
            if (visitorCounter == null)
                throw new ArgumentNullException(nameof(visitorCounter));

            VisitorImageUIContainer visitorImageUIContainer = null;
            
            if(visitorView is VisitorView concrete)
                visitorImageUIContainer = concrete.GetComponentInChildren<VisitorImageUIContainer>();
            
            _imageUIFactory.Create(visitorImageUIContainer.OrderImage);
            _imageUIFactory.Create(visitorImageUIContainer.BackGroundImage);

            VisitorInitializeState initializeState = new VisitorInitializeState(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorIdleState idleState = new VisitorIdleState(
                visitor, visitorAnimation);
            VisitorMoveToSeat moveToSeatState = new VisitorMoveToSeat(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorSeatState visitorSeatState = new VisitorSeatState(
                visitorView, visitor, visitorAnimation, tavernMood);
            VisitorWaitingForOrderState visitorWaitingForOrderState =
                new VisitorWaitingForOrderState(visitor,
                    visitorInventory, visitorImageUIContainer,
                    _productShuffleService, tavernMood);
            VisitorEatFoodState visitorEatFoodState = new VisitorEatFoodState(
                visitor, visitorInventory, visitorImageUIContainer, 
                _itemViewFactory, tavernMood, _garbageSpawner, _coinSpawner);
            VisitorMoveToExitState visitorMoveToExitState = new VisitorMoveToExitState(
                visitorView, visitor, visitorAnimation, _collectionRepository,
                visitorInventory, visitorImageUIContainer);
            VisitorNotSatisfiedWithOrderState visitorNotSatisfiedWithOrderState =
                new VisitorNotSatisfiedWithOrderState(visitor);
            VisitorReturnToPoolState visitorReturnToPoolState = new VisitorReturnToPoolState(
                visitorView, visitor, visitorCounter);

            FiniteTransitionBase toMoveToSeatTransition = new FiniteTransitionBase(
                moveToSeatState, () => visitor.TargetPosition != null);
            initializeState.AddTransition(toMoveToSeatTransition);

            FiniteTransitionBase toSeatIdleTransition = new FiniteTransitionBase(
                visitorSeatState, () => Vector3.Distance(visitorView.Position,
                    visitor.SeatPointView.Position) <= visitorView.NavMeshAgent.stoppingDistance);
            moveToSeatState.AddTransition(toSeatIdleTransition);

            FiniteTransitionBase toWaitingForOrderTransition = new FiniteTransitionBase(
                visitorWaitingForOrderState, () => visitor.CanSeat);
            visitorSeatState.AddTransition(toWaitingForOrderTransition);

            FiniteTransitionBase toEatFoodTransition = new FiniteTransitionBase(
                visitorEatFoodState, () => visitorInventory.Item != null);
            visitorWaitingForOrderState.AddTransition(toEatFoodTransition);

            FiniteTransitionBase toMoveExitState = new FiniteTransitionBase(
                visitorMoveToExitState, () => visitor.CanSeat == false);
            visitorEatFoodState.AddTransition(toMoveExitState);

            FiniteTransitionBase toNotSatisfiedWithOrderState = new FiniteTransitionBase(
                visitorNotSatisfiedWithOrderState, () => visitor.IsUnhappy);
            visitorWaitingForOrderState.AddTransition(toNotSatisfiedWithOrderState);

            FiniteTransitionBase fromNotSatisfiedWithOrderToMoveExitState =
                new FiniteTransitionBase(visitorMoveToExitState, () =>
                    visitorInventory.Item == null);
            visitorNotSatisfiedWithOrderState.AddTransition(fromNotSatisfiedWithOrderToMoveExitState);

            FiniteTransitionBase toReturnToPoolState = new FiniteTransitionBase(
                visitorReturnToPoolState, () => visitor.IsIdle);
            visitorMoveToExitState.AddTransition(toReturnToPoolState);
            // FiniteTransitionBase toIdleTransition = new FiniteTransitionBase(
            //     idleState, () => visitor.IsIdle);
            // moveToSeatState.AddTransition(toIdleTransition);

            return new VisitorPresenter(initializeState, visitorView, visitor);
        }
    }
}