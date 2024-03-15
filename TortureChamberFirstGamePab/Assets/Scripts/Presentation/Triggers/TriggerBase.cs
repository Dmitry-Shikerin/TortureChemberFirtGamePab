using System;
using UnityEngine;

namespace Scripts.Presentation.Triggers
{
    public class TriggerBase<T> : MonoBehaviour
    {
        public event Action<T> Entered;
        public event Action<T> Exited;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out T component))
                Entered?.Invoke(component);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out T component))
                Exited?.Invoke(component);
        }
    }
}