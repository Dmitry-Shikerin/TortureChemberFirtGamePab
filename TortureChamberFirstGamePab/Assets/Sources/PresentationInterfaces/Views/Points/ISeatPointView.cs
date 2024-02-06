﻿using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

namespace Sources.PresentationInterfaces.Views.Points
{
    public interface ISeatPointView
    {
        public EatPointView EatPointView { get; }
        public bool IsOccupied { get; }

        void Occupy();
        void UnOccupy();
    }
    
}