using System;
using System.Threading;
using Sources.Presentation.Views.Items.Garbages;
using UnityEngine;

namespace MyProject.Sources.Presentation.Views.Triggers.Items
{
    public class GarbageTrigger : MonoBehaviour
    {
        private GarbageView _garbageView;
        
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _garbageView = GetComponentInParent<GarbageView>() ?? 
                           GetComponent<GarbageView>() ??
                           throw new NullReferenceException(nameof(GarbageView));
        }

        private void OnTriggerEnter(Collider other)
        {
            //TDOD нужно будет запускать анимацию у персонажа и выключить движение
            if (other.gameObject.TryGetComponent(out PlayerMovementView playerMovementView))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                
                _garbageView.CleanUp(_cancellationTokenSource.Token);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovementView playerMovementView))
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}