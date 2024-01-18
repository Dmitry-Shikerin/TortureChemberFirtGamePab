﻿using System;
using JetBrains.Annotations;
using Sources.Controllers.Points;
using Sources.Domain.Points;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Infrastructure.Factories.Controllers.Points
{
    public class SeatPointPresenterFactory
    {
        public SeatPointPresenter Create(ISeatPointView seatPointView, SeatPoint seatPoint)
        {
            if (seatPointView == null) 
                throw new ArgumentNullException(nameof(seatPointView));
            
            if (seatPoint == null) 
                throw new ArgumentNullException(nameof(seatPoint));
            
            return new SeatPointPresenter
            (
                seatPointView,
                seatPoint
            );
        }
    }
}