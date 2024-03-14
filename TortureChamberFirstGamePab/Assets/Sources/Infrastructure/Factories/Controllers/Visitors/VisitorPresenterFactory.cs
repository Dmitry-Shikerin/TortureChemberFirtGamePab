using System;
using Sources.Controllers.Visitors;
using Sources.Controllers.Visitors.States;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Sources.InfrastructureInterfaces.Spawners;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository.Interfaces;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Controllers.Visitors
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

            var initializeState = new VisitorInitializeState(
                visitorView,
                visitor,
                _collectionRepository,
                visitorView.VisitorImageUIContainer);
            var moveToSeatState = new VisitorMoveToSeat(
                visitorView,
                visitor,
                visitorAnimation);
            var visitorSeatState = new VisitorSeatState(
                visitorView,
                visitor,
                visitorAnimation,
                tavernMood);
            var visitorWaitingForOrderState =
                new VisitorWaitingForOrderState(visitor,
                    visitorInventory,
                    visitorView.VisitorImageUIContainer,
                    tavernMood,
                    visitorAnimation,
                    visitorView,
                    _itemProvider);
            var visitorEatFoodState = new VisitorEatFoodState(
                visitor,
                visitorInventory,
                visitorView.VisitorImageUIContainer,
                _itemViewFactory,
                tavernMood,
                _garbageSpawner,
                _coinSpawner);
            var visitorMoveToExitState = new VisitorMoveToExitState(
                visitorView,
                visitor,
                visitorAnimation,
                _collectionRepository,
                visitorView.VisitorImageUIContainer);
            var visitorNotSatisfiedWithOrderState =
                new VisitorNotSatisfiedWithOrderState(visitor, tavernMood);
            var visitorReturnToPoolState = new VisitorReturnToPoolState(
                visitorView,
                visitor,
                visitorCounter);

            var toMoveToSeatTransition = new FiniteTransitionBase(
                moveToSeatState,
                () => visitor.TargetPosition != null);
            initializeState.AddTransition(toMoveToSeatTransition);

            var toSeatIdleTransition = new FiniteTransitionBase(
                visitorSeatState,
                () => Vector3.Distance(visitorView.Position,
                    visitor.SeatPointView.Position) <= visitorView.NavMeshAgent.stoppingDistance);
            moveToSeatState.AddTransition(toSeatIdleTransition);

            var toWaitingForOrderTransition = new FiniteTransitionBase(
                visitorWaitingForOrderState,
                () => visitor.CanSeat);
            visitorSeatState.AddTransition(toWaitingForOrderTransition);

            var toEatFoodTransition = new FiniteTransitionBase(
                visitorEatFoodState,
                () => visitorInventory.Item != null);
            visitorWaitingForOrderState.AddTransition(toEatFoodTransition);

            var toMoveExitState = new FiniteTransitionBase(
                visitorMoveToExitState,
                () => visitor.CanSeat == false);
            visitorEatFoodState.AddTransition(toMoveExitState);

            var toNotSatisfiedWithOrderState = new FiniteTransitionBase(
                visitorNotSatisfiedWithOrderState,
                () => visitor.IsUnhappy);
            visitorWaitingForOrderState.AddTransition(toNotSatisfiedWithOrderState);

            var fromNotSatisfiedWithOrderToMoveExitState =
                new FiniteTransitionBase(visitorMoveToExitState,
                    () =>
                        visitorInventory.Item == null);
            visitorNotSatisfiedWithOrderState.AddTransition(fromNotSatisfiedWithOrderToMoveExitState);

            var toReturnToPoolState = new FiniteTransitionBase(
                visitorReturnToPoolState,
                () => visitor.IsIdle);
            visitorMoveToExitState.AddTransition(toReturnToPoolState);

            return new VisitorPresenter(initializeState, visitorView, visitor, _updateServiceChanger);
        }
    }
}