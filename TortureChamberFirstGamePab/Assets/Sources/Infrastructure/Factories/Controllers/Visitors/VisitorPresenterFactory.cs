using System;
using JetBrains.Annotations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers;
using Sources.Controllers.Visitors.States;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.Transitions;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Infrastructure.Factorys.Controllers
{
    public class VisitorPresenterFactory
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly ProductShuffleService _productShuffleService;

        public VisitorPresenterFactory(CollectionRepository collectionRepository,
            ProductShuffleService productShuffleService)
        {
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _productShuffleService = productShuffleService ?? throw new ArgumentNullException(nameof(productShuffleService));
        }
        
        public VisitorPresenter Create(IVisitorView visitorView,
            IVisitorAnimation visitorAnimation, Visitor visitor,
            ItemRepository<IItem> itemRepository, VisitorImageUI visitorImageUI,
            VisitorInventory visitorInventory, ImageUIFactory imageUIFactory,
            ItemViewFactory itemViewFactory, TavernMood tavernMood, GarbageBuilder garbageBuilder,
            CoinBuilder coinBuilder, VisitorCounter visitorCounter)
        {
            if (visitorView == null) 
                throw new ArgumentNullException(nameof(visitorView));
            if (visitorAnimation == null) 
                throw new ArgumentNullException(nameof(visitorAnimation));
            if (visitor == null) 
                throw new ArgumentNullException(nameof(visitor));
            if (itemRepository == null) 
                throw new ArgumentNullException(nameof(itemRepository));
            if (visitorImageUI == null) 
                throw new ArgumentNullException(nameof(visitorImageUI));
            if (visitorInventory == null)
                throw new ArgumentNullException(nameof(visitorInventory));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));
            if (itemViewFactory == null)
                throw new ArgumentNullException(nameof(itemViewFactory));
            if (tavernMood == null) 
                throw new ArgumentNullException(nameof(tavernMood));
            if (visitorCounter == null) 
                throw new ArgumentNullException(nameof(visitorCounter));

            imageUIFactory.Create(visitorImageUI.OrderImage);
            imageUIFactory.Create(visitorImageUI.BackGroundImage);
            
            VisitorInitializeState initializeState = new VisitorInitializeState(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorIdleState idleState = new VisitorIdleState(
                visitorView,  visitor, visitorAnimation,_collectionRepository);
            VisitorMoveToSeat moveToSeatState = new VisitorMoveToSeat(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorSeatState visitorSeatState = new VisitorSeatState(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorWaitingForOrderState visitorWaitingForOrderState =
                new VisitorWaitingForOrderState(visitor,
                    visitorInventory, visitorImageUI, itemRepository,
                    _productShuffleService, tavernMood);
            VisitorEatFoodState visitorEatFoodState = new VisitorEatFoodState(
                visitorView, visitor, visitorAnimation, _collectionRepository,
                visitorInventory, visitorImageUI, itemViewFactory, tavernMood, garbageBuilder,
                coinBuilder);
            VisitorMoveToExitState visitorMoveToExitState = new VisitorMoveToExitState(
                visitorView, visitor, visitorAnimation, _collectionRepository,
                visitorInventory, visitorImageUI);
            VisitorNotSatisfiedWithOrderState visitorNotSatisfiedWithOrderState =
                new VisitorNotSatisfiedWithOrderState(visitorView, visitor,
                    visitorAnimation, _collectionRepository,
                    visitorInventory, visitorImageUI);
            VisitorReturnToPoolState visitorReturnToPoolState = new VisitorReturnToPoolState(
                visitorView, visitor,
                visitorAnimation, _collectionRepository,
                visitorInventory, visitorImageUI, visitorCounter);

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
