using System;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToSeat : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;

        public VisitorMoveToSeat
        (
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository
        )
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
        }

        public override void Enter()
        {
            // Debug.Log("Посетитель в Состоянии Движения");
            MovingAsync();
        }

        public override void Exit()
        {
        }

        private async void MovingAsync()
        {
            _visitorAnimation.PlayWalk();
            await MoveAsync();
            _visitor.SetIdle();
        }

        private async UniTask MoveAsync()
        {
            _visitorView.SetDestination(_visitor.SeatPointView.Position);

            while (Vector3.Distance(_visitorView.Position, _visitor.SeatPointView.Position) >
                   _visitorView.NavMeshAgent.stoppingDistance)
            {
                await UniTask.Yield();
            }
        }
    }
}