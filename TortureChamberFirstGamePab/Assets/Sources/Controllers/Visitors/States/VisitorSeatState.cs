using System;
using JetBrains.Annotations;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorSeatState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly TavernMood _tavernMood;

        public VisitorSeatState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepository collectionRepository,
            TavernMood tavernMood)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
        }
        
        public override void Enter()
        {
            // Debug.Log("Посетитель в состоянии сидя");
            _visitorView.SeatDown(_visitor.SeatPointView.Position, _visitor.SeatPointView.Rotation);

            if (_visitor.SeatPointView.EatPointView.IsClear == false)
            {
                _tavernMood.RemoveTavernMood();
                Debug.Log("Посетитель недоволен грязным местом");
            }
                
            _visitorAnimation.PlaySeatIdle();
            _visitor.SetSeat();
        }

        public override void Exit()
        {
        }
    }
}