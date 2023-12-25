using System;
using System.Threading;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;

namespace Sources.Presentation.Views.Player.Triggers
{
    public class GettableTrigger : MonoBehaviour
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
            if (other.gameObject.TryGetComponent<IGettable>(out IGettable gettable))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                
                IItem targetItem = gettable.GetTargetItem();

                if (targetItem == null)
                    return;

                if (gettable.GetItem() != null)
                    return;
                
                if(_playerInventoryView.TryGet() == false)
                    return;
                
                IItem item = await _playerInventoryView.GetItem(targetItem, _cancellationTokenSource.Token);
                // IItem item = await _playerInventoryView.GetItem(targetItem, _cancellationTokenSource.Token);
                gettable.Add(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<IGettable>(out IGettable takeble))
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}