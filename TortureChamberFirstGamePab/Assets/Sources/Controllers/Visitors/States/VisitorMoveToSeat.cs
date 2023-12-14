﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToSeat : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepozitory _collectionRepozitory;

        public VisitorMoveToSeat(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation ,CollectionRepozitory collectionRepozitory)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ?? 
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepozitory = collectionRepozitory ?? 
                                   throw new ArgumentNullException(nameof(collectionRepozitory));
        }
        
        public override void Enter()
        {
            Debug.Log("Посетитель в Состоянии Движения");
            Moving();
        }

        public override void Exit()
        {
        }

        private async void Moving()
        {
            _visitorAnimation.PlayWalk();
            await Move();
            _visitor.SetIdle(true);
        }
        
        private async UniTask Move()
        {
            IVisitorPoint seatPoint = _collectionRepozitory.Get<SeatPoint>().FirstOrDefault();
            
            _visitorView.SetDestination(seatPoint.Position);

            while (Vector3.Distance(_visitorView.Position, seatPoint.Position) > 
                   _visitorView.NavMeshAgent.stoppingDistance)
            {
                await UniTask.Yield();
            }
        } 
    }
}