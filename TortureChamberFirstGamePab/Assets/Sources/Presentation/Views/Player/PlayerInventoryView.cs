using System;
using System.Threading;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Taverns;
using UnityEngine;

namespace Sources.Presentation.Views.Player
{
    public class PlayerInventoryView : PresentableView<PlayerInventoryPresenter>, IPlayerInventoryView
    {
        [field : SerializeField] public Transform FirstSlot { get; private set; }
        [field : SerializeField] public Transform SecondSlot { get; private set; }
        [field : SerializeField] public Transform ThirdSlot { get; private set; }

        private CancellationTokenSource _cancellationTokenSource;
        
        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeble>(out ITakeble takeble))
            {
                _cancellationTokenSource = new CancellationTokenSource();

                Debug.Log("Подошел к триггеру");
                IItem item = await takeble.TakeItemAsync(_cancellationTokenSource.Token);
                
                if(item == null)
                    return;
                
                Presenter.AddItem(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeble>(out ITakeble takeble))
            {
                Debug.Log("Вышел из триггера");
                _cancellationTokenSource.Cancel();
            }
        }

        public void Add()
        {
        }
    }
}