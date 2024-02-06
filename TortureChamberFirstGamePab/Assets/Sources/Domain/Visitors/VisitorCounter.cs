using System;
using Sources.Domain.Points;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Domain.Visitors
{
    public class VisitorCounter
    {
        public int ActiveVisitorsCount { get; private set; } = 0;
        
        public void AddActiveVisitorsCount() => 
            ActiveVisitorsCount++;

        public void RemoveActiveVisitor()
        {
            if (ActiveVisitorsCount <= 0)
                throw new InvalidOperationException(nameof(ActiveVisitorsCount));
            
            ActiveVisitorsCount--;
            Debug.Log($"Активных посетителей {ActiveVisitorsCount}");
        }
    }
}