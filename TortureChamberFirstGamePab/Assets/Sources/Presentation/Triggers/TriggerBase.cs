using System;
using Sources.PresentationInterfaces.Triggers;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns
{
    //TODO какое обобщение у тишки?
    //TODO возможно интерфейс не нужен
    public class TriggerBase<T> : MonoBehaviour, ITrigger
    {
        public Action<T> Entered;
        public Action<T> Exited;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out T @object))
            {
                Entered?.Invoke(@object);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out T @object))
            {
                Exited?.Invoke(@object);
            }
        }
    }
}