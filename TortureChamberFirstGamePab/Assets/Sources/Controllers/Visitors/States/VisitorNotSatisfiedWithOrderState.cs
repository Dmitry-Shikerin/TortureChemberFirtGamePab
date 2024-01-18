using System;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorNotSatisfiedWithOrderState : FiniteState
    {
        private readonly Visitor _visitor;

        public VisitorNotSatisfiedWithOrderState(Visitor visitor)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
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