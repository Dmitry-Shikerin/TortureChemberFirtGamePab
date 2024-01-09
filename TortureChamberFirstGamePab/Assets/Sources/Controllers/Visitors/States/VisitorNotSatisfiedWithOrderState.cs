using System;
using System.Threading;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorNotSatisfiedWithOrderState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorInventory _visitorInventory;
        private readonly VisitorImageUI _visitorImageUI;

        public VisitorNotSatisfiedWithOrderState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation, CollectionRepository collectionRepository,
            VisitorInventory visitorInventory, VisitorImageUI visitorImageUI)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorInventory = visitorInventory ?? throw new ArgumentNullException(nameof(visitorInventory));
            _visitorImageUI = visitorImageUI ? visitorImageUI : throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии недоволен заказом");
            _visitor.SetUnHappy();
        }

        public override void Exit()
        {
        }
    }
}