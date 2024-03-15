using Scripts.InfrastructureInterfaces.Services.ObjectPolls;
using UnityEngine;

namespace Scripts.Presentation.Views.ObjectPolls
{
    public class PoolableObject : MonoBehaviour
    {
        private IObjectPool _pool;

        public PoolableObject SetPool(IObjectPool pool)
        {
            _pool = pool;

            return this;
        }

        public void ReturnTooPool() =>
            _pool.Return(this);
    }
}