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
        private readonly CollectionRepozitory _collectionRepozitory;

        public VisitorPresenterFactory(CollectionRepozitory collectionRepozitory)
        {
            _collectionRepozitory = collectionRepozitory ?? 
                                   throw new ArgumentNullException(nameof(collectionRepozitory));
        }
        
        public VisitorPresenter Create(IVisitorView visitorView, 
            IVisitorAnimation visitorAnimation, Visitor visitor)
        {
            VisitorInitializeState initializeState = new VisitorInitializeState(
                visitorView, visitor, visitorAnimation, _collectionRepozitory);
            VisitorIdleState idleState = new VisitorIdleState(
                visitorView,  visitor, visitorAnimation,_collectionRepozitory);
            VisitorMoveToSeat moveToSeatState = new VisitorMoveToSeat(
                visitorView, visitor, visitorAnimation, _collectionRepozitory);
            VisitorSeatState visitorSeatState = new VisitorSeatState(
                visitorView, visitor, visitorAnimation, _collectionRepozitory);

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
