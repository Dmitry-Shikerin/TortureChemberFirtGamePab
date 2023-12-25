using System;
using System.Threading;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Taverns;
using UnityEngine;

namespace Sources.Presentation.Views.Player.Triggers
{
    public class TakebleTrigger : MonoBehaviour
    {
        private PlayerInventoryView _playerInventoryView;
        private CancellationTokenSource _cancellationTokenSource;
        
        private void Awake()
        {
            _playerInventoryView = GetComponentInParent<PlayerInventoryView>() ?? 
                                   throw new NullReferenceException(nameof(_playerInventoryView));
        }
        
        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeble>(out ITakeble takeble))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                //TODO прокинуть до презентера инвентаря
                IItem item = await takeble.TakeItem(_cancellationTokenSource.Token);
                _playerInventoryView.AddItem(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeble>(out ITakeble takeble))
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}