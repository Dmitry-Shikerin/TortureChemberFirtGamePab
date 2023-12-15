using System;
using JetBrains.Annotations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers;
using Sources.Controllers.Visitors.States;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.Transitions;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Infrastructure.Factorys.Controllers
{
    public class VisitorPresenterFactory
    {
        private readonly CollectionRepository _collectionRepository;

        public VisitorPresenterFactory(CollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository ?? 
                                   throw new ArgumentNullException(nameof(collectionRepository));
        }
        
        public VisitorPresenter Create(IVisitorView visitorView, 
            IVisitorAnimation visitorAnimation, Visitor visitor)
        {
            VisitorInitializeState initializeState = new VisitorInitializeState(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorIdleState idleState = new VisitorIdleState(
                visitorView,  visitor, visitorAnimation,_collectionRepository);
            VisitorMoveToSeat moveToSeatState = new VisitorMoveToSeat(
                visitorView, visitor, visitorAnimation, _collectionRepository);
            VisitorSeatState visitorSeatState = new VisitorSeatState(
                visitorView, visitor, visitorAnimation, _collectionRepository);

            FiniteTransitionBase toMoveToSeatTransition = new FiniteTransitionBase(
                moveToSeatState, () => visitor.TargetPosition != null);
            initializeState.AddTransition(toMoveToSeatTransition);

            FiniteTransitionBase toSeatIdleTransition = new FiniteTransitionBase(
                visitorSeatState, () => Vector3.Distance(visitorView.Position,
                    visitor.SeatPoint.Position) <= visitorView.NavMeshAgent.stoppingDistance);
            moveToSeatState.AddTransition(toSeatIdleTransition);
            
            // FiniteTransitionBase toIdleTransition = new FiniteTransitionBase(
            //     idleState, () => visitor.IsIdle);
            // moveToSeatState.AddTransition(toIdleTransition);
            
            return new VisitorPresenter(initializeState, visitorView, visitor);
        }
    }
}
