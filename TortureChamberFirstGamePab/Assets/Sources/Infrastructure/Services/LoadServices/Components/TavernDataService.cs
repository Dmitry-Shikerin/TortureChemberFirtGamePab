using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;
using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class TavernDataService : IDataService<Tavern>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.TavernDataKey.TavernMoodKey);

        public Tavern Load() =>
            new(LoadTavernMood(), LoadGamePlay());

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
            string json = PlayerPrefs.GetString(Constant.TavernDataKey.TavernMoodKey, string.Empty);
            TavernMoodData moodData = JsonConvert.DeserializeObject<TavernMoodData>(json);

            return new TavernMood(moodData);
        }

        private VisitorQuantity LoadGamePlay()
        {
            string json = PlayerPrefs.GetString(Constant.TavernDataKey.VisitorQuantityKey, string.Empty);
            GameplayData gameplayData = JsonConvert.DeserializeObject<GameplayData>(json);

            return new VisitorQuantity(gameplayData);
        }

        private void Save(TavernMood tavernMood)
        {
            string json = JsonConvert.SerializeObject(
                new TavernMoodData() { MoodValue = tavernMood.TavernMoodValue });
            PlayerPrefs.SetString(Constant.TavernDataKey.TavernMoodKey, json);
        }

        private void Save(VisitorQuantity visitorQuantity)
        {
            string json = JsonConvert.SerializeObject(
                new GameplayData() { MaximumVisitorsCapacity = visitorQuantity.MaximumVisitorsQuantity });
            PlayerPrefs.SetString(Constant.TavernDataKey.VisitorQuantityKey, json);
        }
    }
}