using System;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorNotSatisfiedWithOrderState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly TavernMood _tavernMood;

        public VisitorNotSatisfiedWithOrderState(Visitor visitor, TavernMood tavernMood)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
        }

        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии недоволен заказом");
            _visitor.SetUnHappy();
            _tavernMood.RemoveTavernMood();
        }

        public override void Exit()
        {
        }
    }
}