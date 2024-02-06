using System.Collections.Generic;
using Sources.Domain.Constants;
using Sources.Domain.Points;
using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;

namespace Sources.Domain.GamePlays
{
    public class GamePlay
    {
        public GamePlay() : this(Constant.Visitors.MaximumCapacity)
        {
        }
        
        public GamePlay(GameplayData gameplayData) : this(gameplayData.MaximumVisitorsCapacity)
        {
        }
        
        private GamePlay(int maximumVisitorsCapacity)
        {
            MaximumVisitorsCapacity = maximumVisitorsCapacity;
        }
        
        public int MaximumVisitorsCapacity { get; private set; }

        public void AddMaximumVisitorsCapacity() => 
            MaximumVisitorsCapacity++;
    }
}