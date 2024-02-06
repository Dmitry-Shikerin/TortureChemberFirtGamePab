using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.PauseServices;
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
        private readonly IPauseService _pauseService;

        public VisitorMoveToSeat
        (
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository,
            IPauseService pauseService
        )
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
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
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            
            _visitorView.SetDestination(_visitor.SeatPointView.Position);

            while (Vector3.Distance(_visitorView.Position, _visitor.SeatPointView.Position) >
                   _visitorView.NavMeshAgent.stoppingDistance)
            {
                // if (_pauseService.IsPaused)
                // {
                //     _visitorView.StopMove();
                //     _visitorAnimation.PlayIdle();
                // }
                //     
                // await _pauseService.Yield(cancellationTokenSource.Token);
                //
                // _visitorView.Move();
                // _visitorAnimation.PlayWalk();

                await UniTask.Yield();
            }
        }
    }
}