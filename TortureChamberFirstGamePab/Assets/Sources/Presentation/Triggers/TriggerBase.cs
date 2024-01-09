using System;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns
{
    public class TriggerBase<T> : MonoBehaviour
    {
        public Action<T> Entered;
        public Action<T> Exited;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out T component))
            {
                Entered?.Invoke(component);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out T component))
            {
                Exited?.Invoke(component);
            }
        }
    }
}