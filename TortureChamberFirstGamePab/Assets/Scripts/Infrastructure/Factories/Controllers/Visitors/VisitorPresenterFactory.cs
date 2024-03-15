using System;
using Scripts.Controllers.Visitors;
using Scripts.Controllers.Visitors.States;
using Scripts.Domain.Taverns;
using Scripts.Domain.Visitors;
using Scripts.DomainInterfaces.Items;
using Scripts.Infrastructure.Factories.Views.Items.Common;
using Scripts.Infrastructure.StateMachines.FiniteStateMachines.Transitions;
using Scripts.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Scripts.InfrastructureInterfaces.Spawners;
using Scripts.PresentationInterfaces.Animations;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Items.Garbages;
using Scripts.PresentationInterfaces.Views.Visitors;
using Scripts.Utils.Repositories.CollectionRepository;
using Scripts.Utils.Repositories.ItemRepository.Interfaces;
using UnityEngine;

namespace Scripts.Infrastructure.Factories.Controllers.Visitors
{
    public class VisitorPresenterFactory
    {
        private readonly ISpawner<ICoinView> _coinSpawner;
        private readonly CollectionRepository _collectionRepository;
        private readonly ISpawner<IGarbageView> _garbageSpawner;
        private readonly IItemProvider<IItem> _itemProvider;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly IUpdateServiceChanger _updateServiceChanger;

        public VisitorPresenterFactory(
            IUpdateServiceChanger updateServiceChanger,
            CollectionRepository collectionRepository,
            ItemViewFactory itemViewFactory,
            ISpawner<IGarbageView> garbageSpawner,
            ISpawner<ICoinView> coinSpawner,
            IItemProvider<IItem> itemProvider)
        {
            _updateServiceChanger = updateServiceChanger ??
                                    throw new ArgumentNullException(nameof(updateServiceChanger));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _garbageSpawner = garbageSpawner ?? throw new ArgumentNullException(nameof(garbageSpawner));
            _coinSpawner = coinSpawner ?? throw new ArgumentNullException(nameof(coinSpawner));
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
        }

        public VisitorPresenter Create(
            IVisitorView visitorView,
            IVisitorAnimation visitorAnimation,
            Visitor visitor,
            VisitorInventory visitorInventory,
            TavernMood tavernMood,
            VisitorCounter visitorCounter)
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

            VisitorInitializeState initializeState = new VisitorInitializeState(
                visitorView, visitor, _collectionRepository, visitorView.VisitorImageUIContainer);
            VisitorMoveToSeat moveToSeatState = new VisitorMoveToSeat(
                visitorView, visitor, visitorAnimation);
            VisitorSeatState visitorSeatState = new VisitorSeatState(
                visitorView, visitor, visitorAnimation, tavernMood);
            VisitorWaitingForOrderState visitorWaitingForOrderState = new VisitorWaitingForOrderState(
                    visitor,
                    visitorInventory,
                    visitorView.VisitorImageUIContainer,
                    visitorAnimation,
                    _itemProvider);
            VisitorEatFoodState visitorEatFoodState = new VisitorEatFoodState(
                visitor,
                visitorInventory,
                visitorView.VisitorImageUIContainer,
                _itemViewFactory,
                tavernMood,
                _garbageSpawner,
                _coinSpawner);
            VisitorMoveToExitState visitorMoveToExitState = new VisitorMoveToExitState(
                visitorView,
                visitor,
                visitorAnimation,
                _collectionRepository,
                visitorView.VisitorImageUIContainer);
            VisitorNotSatisfiedWithOrderState visitorNotSatisfiedWithOrderState =
                new VisitorNotSatisfiedWithOrderState(visitor, tavernMood);
            VisitorReturnToPoolState visitorReturnToPoolState = new VisitorReturnToPoolState(
                visitorView, visitor, visitorCounter);

            FiniteTransitionBase toMoveToSeatTransition = new FiniteTransitionBase(
                moveToSeatState, () => visitor.TargetPosition != visitorView.Position);
            initializeState.AddTransition(toMoveToSeatTransition);

            FiniteTransitionBase toSeatIdleTransition = new FiniteTransitionBase(
                visitorSeatState, () => Vector3.Distance(
                    visitorView.Position,
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
                new FiniteTransitionBase(visitorMoveToExitState, () => visitorInventory.Item == null);
            visitorNotSatisfiedWithOrderState.AddTransition(fromNotSatisfiedWithOrderToMoveExitState);

            FiniteTransitionBase toReturnToPoolState = new FiniteTransitionBase(
                visitorReturnToPoolState, () => visitor.IsIdle);
            visitorMoveToExitState.AddTransition(toReturnToPoolState);

            return new VisitorPresenter(initializeState, _updateServiceChanger);
        }
    }
}