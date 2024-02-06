using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToExitState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly IPauseService _pauseService;
        private readonly VisitorImageUIContainer _visitorImageUIContainer;

        public VisitorMoveToExitState
        (
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository,
            VisitorInventory visitorInventory,
            VisitorImageUIContainer visitorImageUIContainer,
            IPauseService pauseService
        )
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _visitorImageUIContainer = visitorImageUIContainer
                ? visitorImageUIContainer
                : throw new ArgumentNullException(nameof(visitorImageUIContainer));
        }

        public override void Enter()
        {
            _visitorImageUIContainer.BackGroundImage.HideImage();
            _visitorImageUIContainer.OrderImage.HideImage();
            
            MovingAsync();
        }

        public override void Exit()
        {
        }

        private async void MovingAsync()
        {
            _visitor.SetMove();
            _visitorAnimation.PlayStandUp();
            _visitorAnimation.PlayWalk();
            await MoveAsync();
            _visitor.SetIdle();
            _visitorView.StopMove();
        }

        private async UniTask MoveAsync()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            
            IVisitorPoint outDoorPoint = _collectionRepository.Get<OutDoorPoint>().FirstOrDefault();

            _visitorView.SetDestination(outDoorPoint.Position);

            while (Vector3.Distance(_visitorView.Position, outDoorPoint.Position) >
                   _visitorView.NavMeshAgent.stoppingDistance)
            {
                if (_pauseService.IsPaused)
                {
                    _visitorView.StopMove();
                    _visitorAnimation.PlayIdle();
                }
                    
                await _pauseService.Yield(cancellationTokenSource.Token);
                
                _visitorView.Move();
                _visitorAnimation.PlayWalk();
            }
        }
    }
}