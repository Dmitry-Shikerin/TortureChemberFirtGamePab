using Sources.Controllers.YandexSDC;
using Sources.PresentationInterfaces.Views.YandexSDC;
using TMPro;
using UnityEngine;

namespace Sources.Presentation.Views.YandexSDC.MyVariant
{
    public class LeaderboardElementView : PresentableView<LeaderboardElementPresenter>, ILeaderboardElementView
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;

        public void SetName(string name) => 
            _playerName.text = name;

        public void SetRank(string rank) => 
            _playerRank.text = rank;

        public void SetScore(string score) => 
            _playerScore.text = score;
    }
}