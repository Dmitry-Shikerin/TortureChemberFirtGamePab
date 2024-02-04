using System;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.ObjectPolls;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorReturnToPoolState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly VisitorCounter _visitorCounter;
        private readonly IObjectPool _objectPool;

        public VisitorReturnToPoolState
        (
            IVisitorView visitorView,
            Visitor visitor,
            VisitorCounter visitorCounter
        )
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
        }

        public override void Enter()
        {
            _visitor.FinishEating();
            _visitor.SetHappy();
            _visitor.SetIdle();
            _visitor.SetSeatPoint(null);
            _visitor.SetUnSeat();

            _visitorCounter.RemoveActiveVisitor();

            _visitorView.Destroy();
        }

        public override void Exit()
        {
        }
    }
}