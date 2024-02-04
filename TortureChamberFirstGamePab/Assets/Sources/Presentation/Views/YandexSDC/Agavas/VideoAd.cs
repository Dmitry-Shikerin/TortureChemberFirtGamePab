using UnityEngine;
using Agava;

namespace Sources.Presentation.Views.YandexSDC
{
    //TODO сделать сервисом?
    //TODO где вызывать метод Show?
    public class VideoAd : MonoBehaviour
    {
        private int _money;

        public void Show()
        {
            // Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            AudioListener.volume = 0f;
        }

        private void OnRewardCallback()
        {
            _money++;
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1;
            AudioListener.volume = 1f;
        }
    }
}