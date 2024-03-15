using System;
using System.Collections.Generic;
using Scripts.InfrastructureInterfaces.Services.ObjectPolls;
using Scripts.Presentation.Views;
using Scripts.Presentation.Views.ObjectPolls;
using UnityEngine;

namespace Scripts.Infrastructure.Services.ObjectPools
{
    public class ObjectPool<T> : IObjectPool
        where T : View
    {
        private readonly Queue<T> _objects = new ();
        private readonly Transform _parent;

        public ObjectPool()
        {
            _parent = new GameObject($"Pool of {typeof(T).Name}").transform;
        }

        public event Action<int> ObjectCountChanged;

        public TType Get<TType>()
            where TType : View
        {
            if (_objects.Count == 0)
                return null;

            if (_objects.Dequeue() is not TType @object)
                return null;

            if (@object == null)
                return null;

            @object.SetParent(null);
            ObjectCountChanged?.Invoke(_objects.Count);

            @object.Show();

            return @object;
        }

        public void Return(PoolableObject poolableObject)
        {
            if (poolableObject.TryGetComponent(out T @object) == false)
                return;

            poolableObject.transform.SetParent(_parent);
            _objects.Enqueue(@object);
            ObjectCountChanged?.Invoke(_objects.Count);
        }
    }
}