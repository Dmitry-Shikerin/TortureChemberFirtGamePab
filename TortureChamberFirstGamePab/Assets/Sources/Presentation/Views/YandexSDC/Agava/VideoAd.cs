using UnityEngine;
using Agava;

namespace Sources.Presentation.Views.YandexSDC
{
    //TODO сделать сервисом?
    //TODO где вызывать метод Show?
    public class VideoAd : MonoBehaviour
    {
        private int _money;

        //TOdo вынести стаатику
        public void Show()
        {
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);
        }

        private void OnOpenCallback()
        {
            //TODO прокинуть сюда пауз сервис
            Time.timeScale = 0;
            AudioListener.volume = 0f;
        }

        //TODO прокинуть сюда кошелек
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