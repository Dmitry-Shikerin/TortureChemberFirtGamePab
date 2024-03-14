﻿using Sources.Domain.Constants;
using Sources.Domain.DataAccess.TavernData;

namespace Sources.Domain.GamePlays
{
    public class VisitorQuantity
    {
        public VisitorQuantity() : this(Constant.Visitors.MaximumQuantity)
        {
        }

        public VisitorQuantity(GameplayData gameplayData) : this(gameplayData.MaximumVisitorsCapacity)
        {
        }

        private VisitorQuantity(int maximumVisitorsQuantity)
        {
            MaximumVisitorsQuantity = maximumVisitorsQuantity;
        }

        public int MaximumVisitorsQuantity { get; private set; }

        public void AddMaximumVisitorsQuantity()
        {
            MaximumVisitorsQuantity++;
        }
    }
}