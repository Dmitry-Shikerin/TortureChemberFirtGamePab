using System;
using JetBrains.Annotations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorIdleState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;

        public VisitorIdleState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation ,CollectionRepository collectionRepository)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ?? 
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ?? 
                                   throw new ArgumentNullException(nameof(collectionRepository));
        }
        
        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии покоя");
            _visitorAnimation.PlayIdle();
            // SeatPoint seatPoint = _visitorPointService.Get<SeatPoint>();
            // _visitor.SetTargetPosition(seatPoint.transform.position);
            _visitor.SetIdle(false);
        }
        
        public override void Exit()
        {
        }
    }
}