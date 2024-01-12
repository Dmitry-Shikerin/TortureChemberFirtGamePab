using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToExitState : FiniteState
    {
        
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;

        public VisitorMoveToExitState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation, CollectionRepository collectionRepository,
            VisitorInventory visitorInventory, VisitorImageUIContainer visitorImageUIContainer)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
        }

        public override void Enter()
        {
            // Debug.Log("Посетитель в состоянии движения к выходу");
            Moving();
        }

        public override void Exit()
        {
        }
        
        private async void Moving()
        {
            _visitor.SetMove();
            _visitorAnimation.PlayStandUp();
            _visitorAnimation.PlayWalk();
            await Move();
            _visitor.SetIdle();
            _visitorView.StopMove();
        }

        private async UniTask Move()
        {
            IVisitorPoint outDoorPoint = _collectionRepository.Get<OutDoorPoint>().FirstOrDefault();

            _visitorView.SetDestination(outDoorPoint.Position);

            while (Vector3.Distance(_visitorView.Position, outDoorPoint.Position) >
                   _visitorView.NavMeshAgent.stoppingDistance)
            {
                await UniTask.Yield();
            }
        }
    }
}