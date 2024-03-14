using Sources.Domain.Constants;
using Sources.Domain.DataAccess.TavernData;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class TavernDataService : DataServiceBase, IDataService<Tavern>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.TavernDataKey.TavernMoodKey);

        public Tavern Load()
        {
            return new Tavern(LoadTavernMood(), LoadGamePlay());
        }

        public void Save(Tavern @object)
        {
            Save(@object.TavernMood);
            Save(@object.VisitorQuantity);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constant.TavernDataKey.TavernMoodKey);
            PlayerPrefs.DeleteKey(Constant.TavernDataKey.VisitorQuantityKey);
        }

        private TavernMood LoadTavernMood()
        {
            return new TavernMood(LoadData<TavernMoodData>(Constant.TavernDataKey.TavernMoodKey));
        }

        private VisitorQuantity LoadGamePlay()
        {
            return new VisitorQuantity(LoadData<GameplayData>(Constant.TavernDataKey.VisitorQuantityKey));
        }

        private void Save(TavernMood tavernMood)
        {
            var tavernMoodData = new TavernMoodData
            {
                MoodValue = tavernMood.TavernMoodValue
            };

            SaveData(tavernMoodData, Constant.TavernDataKey.TavernMoodKey);
        }

        private void Save(VisitorQuantity visitorQuantity)
        {
            var gameplayData = new GameplayData
            {
                MaximumVisitorsCapacity = visitorQuantity.MaximumVisitorsQuantity
            };

            SaveData(gameplayData, Constant.TavernDataKey.VisitorQuantityKey);
        }
    }
}