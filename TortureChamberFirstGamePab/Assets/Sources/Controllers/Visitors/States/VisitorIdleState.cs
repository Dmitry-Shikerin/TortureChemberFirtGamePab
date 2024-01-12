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
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;

        public VisitorIdleState(Visitor visitor,
            IVisitorAnimation visitorAnimation )
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ?? 
                                throw new ArgumentNullException(nameof(visitorAnimation));
        }
        
        public override void Enter()
        {
            _visitorAnimation.PlayIdle();
            _visitor.SetMove();
        }
        
        public override void Exit()
        {
        }
    }
}