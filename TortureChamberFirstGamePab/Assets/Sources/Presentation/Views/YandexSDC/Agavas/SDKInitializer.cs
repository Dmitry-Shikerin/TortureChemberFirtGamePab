using System;
using System.Collections;
using Agava.YandexGames;
using Sources.Domain.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Presentation.Views.YandexSDC
{
    public class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        //TODO как переделать это очко?
        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene(Constant.SceneNames.MainMenu);
        }
        
        //TODO сделать инициализацию через Zenject
        //Для того, что бы произвести инициализацию СДК. Необходимо на самой первой игровой сцене (Нулевой),
        //которую мы устанавливаем при билде проекта, разместить только один объект с одним компонентом.
        //Его задача дождать от коррутины колбека окончания инициализации и запустить первую игровую сцену.
        //Таким образом, мы точно уверенны, что когда игрок оказывается на первой игровой сцене, его СДК готово
        //к работе и мы можем смело пользоваться функционалом, не боясь ошибок.
        
        //TODO этот метод не отсюда
        //TODO статика
        //На первой игровой сцене, в своём классе вызываем данную функцию и это необходимо по новым правилам
        //модерации яндекс. Она необходима для метрик платформы.
        //TODO это в энтере в конце всего
        public void OnCallGameReadyButtonClick()
        {
            YandexGamesSdk.GameReady();
        }
    }
}