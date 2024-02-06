using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;
using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class TavernDataService : IDataService<Tavern>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.TavernDataKey.TavernMoodKey);
        
        public Tavern Load()
        {
            return new Tavern(LoadTavernMood(), LoadGamePlay());
        }

        public void Save(Tavern @object)
        {
            Save(@object.TavernMood);
            Save(@object.GamePlay);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
        
        private TavernMood LoadTavernMood()
        {
            string json = PlayerPrefs.GetString(Constant.TavernDataKey.TavernMoodKey, string.Empty);
            TavernMoodData moodData = JsonConvert.DeserializeObject<TavernMoodData>(json);

            return new TavernMood(moodData);
        }

        private GamePlay LoadGamePlay()
        {
            string json = PlayerPrefs.GetString(Constant.TavernDataKey.GameplayKey, string.Empty);
            GameplayData gameplayData = JsonConvert.DeserializeObject<GameplayData>(json);
            
            return new GamePlay(gameplayData);
        }

        private void Save(TavernMood tavernMood)
        {
            TavernMoodData tavernMoodData = new TavernMoodData()
            {
                MoodValue = tavernMood.TavernMoodValue,
            };
            
            string json = JsonConvert.SerializeObject(tavernMoodData);
            PlayerPrefs.SetString(Constant.TavernDataKey.TavernMoodKey, json);
        }

        private void Save(GamePlay gamePlay)
        {
            GameplayData gameplayData = new GameplayData()
            {
                MaximumVisitorsCapacity = gamePlay.MaximumVisitorsCapacity,
            };
            
            string json = JsonConvert.SerializeObject(gameplayData);
            PlayerPrefs.SetString(Constant.TavernDataKey.GameplayKey, json);
        }
    }
}