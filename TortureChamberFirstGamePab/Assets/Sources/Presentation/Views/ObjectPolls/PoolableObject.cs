using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Presentation.Views.ObjectPolls
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