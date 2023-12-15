﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JetBrains.Annotations;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorInitializeState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;

        public VisitorInitializeState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepository collectionRepository)
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
            List<SeatPoint> seatPoints = _collectionRepository.Get<SeatPoint>();
            //TODO заменить эту запись
            SeatPoint seatPoint = seatPoints.FirstOrDefault() ?? 
                                  throw new NullReferenceException(nameof(seatPoints));
            
            _visitor.SetTargetPosition(seatPoint.Position);
            _visitor.SetSeatPoint(seatPoint);
        }

        public override void Exit()
        {
        }
    }
}