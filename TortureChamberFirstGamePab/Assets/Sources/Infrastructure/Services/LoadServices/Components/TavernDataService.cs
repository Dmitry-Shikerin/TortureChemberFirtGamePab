using Newtonsoft.Json;
using Sources.Domain.GamePlays;
using Sources.Domain.Players;
using Sources.Domain.Taverns;
using Sources.Domain.Taverns.Data;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class TavernDataService : IDataService<Tavern>
    {
        private const string TavernMoodKey = nameof(TavernMood);
        private const string GameplayKey = nameof(GamePlay);
        
        public bool CanLoad => PlayerPrefs.HasKey(TavernMoodKey);
        
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
            string json = PlayerPrefs.GetString(TavernMoodKey, string.Empty);
            TavernMoodData moodData = JsonConvert.DeserializeObject<TavernMoodData>(json);

            return new TavernMood(moodData);
        }

        private GamePlay LoadGamePlay()
        {
            string json = PlayerPrefs.GetString(GameplayKey, string.Empty);
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
            PlayerPrefs.SetString(TavernMoodKey, json);
        }

        private void Save(GamePlay gamePlay)
        {
            GameplayData gameplayData = new GameplayData()
            {
                MaximumVisitorsCapacity = gamePlay.MaximumVisitorsCapacity,
            };
            
            string json = JsonConvert.SerializeObject(gameplayData);
            PlayerPrefs.SetString(GameplayKey, json);
        }
    }
}