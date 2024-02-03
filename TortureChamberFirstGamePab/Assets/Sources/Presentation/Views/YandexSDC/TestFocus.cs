using System;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexGames.Utility;

namespace Sources.Presentation.Views.YandexSDC
{
    public class TestFocus : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
        }

        //TODO добавить в этот сервис PauseService
        private void OnInBackgroundChangeApp(bool inApp)
        {
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            MuteAudio(isBackground);
            PauseGame(isBackground);
        }

        private void MuteAudio(bool value)
        {
            _audioSource.volume = value ? 0 : 1;
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }
    }
}