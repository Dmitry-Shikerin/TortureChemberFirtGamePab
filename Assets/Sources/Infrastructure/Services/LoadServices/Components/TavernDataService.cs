using Domain.Constants;
using Domain.DataAccess.Containers.Taverns;
using Domain.DataAccess.TavernData;
using Domain.GamePlays;
using Domain.Taverns;
using InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Infrastructure.Services.LoadServices.Components
{
    public class TavernDataService : DataServiceBase, IDataService<Tavern>
    {
        public bool CanLoad => PlayerPrefs.HasKey(TavernDataKey.TavernMoodKey);

        public Tavern Load() =>
            new (LoadTavernMood(), LoadGamePlay());

        public void Save(Tavern @object)
        {
            Save(@object.TavernMood);
            Save(@object.VisitorQuantity);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(TavernDataKey.TavernMoodKey);
            PlayerPrefs.DeleteKey(TavernDataKey.VisitorQuantityKey);
        }

        private TavernMood LoadTavernMood() =>
            new (LoadData<TavernMoodData>(TavernDataKey.TavernMoodKey));

        private VisitorQuantity LoadGamePlay() =>
            new (LoadData<GameplayData>(TavernDataKey.VisitorQuantityKey));

        private void Save(TavernMood tavernMood)
        {
            TavernMoodData tavernMoodData = new TavernMoodData
            {
                MoodValue = tavernMood.TavernMoodValue,
            };

            SaveData(tavernMoodData, TavernDataKey.TavernMoodKey);
        }

        private void Save(VisitorQuantity visitorQuantity)
        {
            GameplayData gameplayData = new GameplayData
            {
                MaximumVisitorsCapacity = visitorQuantity.MaximumVisitorsQuantity,
            };

            SaveData(gameplayData, TavernDataKey.VisitorQuantityKey);
        }
    }
}