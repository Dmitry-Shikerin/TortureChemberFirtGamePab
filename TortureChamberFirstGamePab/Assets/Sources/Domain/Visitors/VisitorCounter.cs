using System;
using Sources.Domain.Points;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Domain.Visitors
{
    public class VisitorCounter
    {
        // private readonly CollectionRepository _collectionRepository;
        //
        // // public VisitorCounter(int freeSeatPointsCount)
        // // {
        // //     FreeSeatPointsCount = freeSeatPointsCount;
        // // }
        // public VisitorCounter(CollectionRepository collectionRepository)
        // {
        //     _collectionRepository = collectionRepository ?? 
        //                             throw new ArgumentNullException(nameof(collectionRepository));
        // }
        //
        // private int MaximumSeatPoints => _collectionRepository.Get<SeatPoint>().Count;
        // public int FreeSeatPointsCount { get; private set; }
        //
        // public int GetFreSeatPointsCount()
        // {
        //     
        // }
        
        // public int FreeSeatPointsCount { get; private set; }
        public int ActiveVisitorsCount { get; private set; } = 0;

        // public void AddFreeSeatPoint()
        // {
        //     FreeSeatPointsCount++;
        // }
        //
        // public void RemoveFreeSeatPoint()
        // {
        //     if (FreeSeatPointsCount <= 0)
        //         throw new InvalidOperationException(nameof(ActiveVisitorsCount));
        //
        //     FreeSeatPointsCount--;
        // }
        
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