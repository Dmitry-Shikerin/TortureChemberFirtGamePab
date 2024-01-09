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
        private readonly VisitorInventory _visitorInventory;
        private readonly VisitorImageUI _visitorImageUI;

        public VisitorMoveToExitState(IVisitorView visitorView, Visitor visitor,
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
            Debug.Log("Посетитель в состоянии движения к выходу");
            
            Moving();
        }

        public override void Exit()
        {
            //TODO сделать сброс всех булок
        }
        
        private async void Moving()
        {
            _visitor.SetMove();
            _visitorAnimation.PlayStandUp();
            _visitorAnimation.PlayWalk();
            await Move();
            //TODO возможно убрать
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
            
            // _visitorView.SetPosition(outDoorPoint.Position);
        }
    }
    

}