using System;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorSeatState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepozitory _collectionRepozitory;

        public VisitorSeatState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepozitory collectionRepozitory)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepozitory = collectionRepozitory ?? 
                                   throw new ArgumentNullException(nameof(collectionRepozitory));
        }
        
        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии сидя");
            _visitorView.SeatDown(_visitor.SeatPoint.Position, _visitor.SeatPoint.Rotation);
            _visitorAnimation.PlaySeatIdle();
        }

        public override void Exit()
        {
        }
    }
}