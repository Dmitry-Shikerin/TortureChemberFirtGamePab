using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorReturnToPoolState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorInventory _visitorInventory;
        private readonly VisitorCounter _visitorCounter;
        private readonly IObjectPool _objectPool;
        private readonly VisitorImageUI _visitorImageUI;

        public VisitorReturnToPoolState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation, CollectionRepository collectionRepository,
            VisitorInventory visitorInventory, VisitorImageUI visitorImageUI,
            VisitorCounter visitorCounter
            )
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorInventory = visitorInventory ?? throw new ArgumentNullException(nameof(visitorInventory));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
            _visitorImageUI = visitorImageUI ? visitorImageUI :
                throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии возврата в пул");
            _visitor.FinishEating();
            _visitor.SetHappy();
            _visitor.SetIdle();
            _visitor.SetSeatPoint(null);
            _visitor.SetUnSeat();
            
            _visitorCounter.RemoveActiveVisitor();
            
            _visitorView.Destroy();
            //TODO не добавляются в паренты к своему пулу
        }

        public override void Exit()
        {
        }
    }
}