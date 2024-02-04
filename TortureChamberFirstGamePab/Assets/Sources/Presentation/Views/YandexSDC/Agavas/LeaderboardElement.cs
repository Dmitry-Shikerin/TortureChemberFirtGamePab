using TMPro;
using UnityEngine;

namespace Sources.Presentation.Views.YandexSDC
{
    public class LeaderboardElement : View
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;

        //TODO переименовать в конструкт
        public void Initialize(string name, int rank, int score)
        {
            _playerName.text = name;
            _playerRank.text = rank.ToString();
            _playerScore.text = score.ToString();
        }
    }
}