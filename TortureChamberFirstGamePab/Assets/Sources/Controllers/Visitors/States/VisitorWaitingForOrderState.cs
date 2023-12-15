using System;
using JetBrains.Annotations;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorImageUIView _visitorImageUIView;

        public VisitorWaitingForOrderState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepository collectionRepository,
            VisitorImageUIView visitorImageUIView)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorImageUIView = visitorImageUIView ? visitorImageUIView : 
                throw new ArgumentNullException(nameof(visitorImageUIView));
        }
        
        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии ожидания заказа");
        }

        public override void Exit()
        {
        }
    }
}