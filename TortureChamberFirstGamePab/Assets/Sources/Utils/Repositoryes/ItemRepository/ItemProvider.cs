using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Utils.Repositoryes.ItemRepository
{
    public class ItemProvider<T1>
    {
        private Dictionary<Type, T1> _repositoryes =
            new Dictionary<Type, T1>();

        public int Count => _repositoryes.Count;

        public T2 Get<T2>() where T2 : T1
        {
            if (_repositoryes.ContainsKey(typeof(T2)) == false)
                throw new InvalidOperationException();

            if (_repositoryes[typeof(T2)] is T2 concrete)
                return concrete;

            throw new InvalidOperationException(nameof(T2));
        }

        public bool TryGetComponent<T2>(out T2 @object) where T2 : T1
        {
            if (_repositoryes.ContainsKey(typeof(T2)) == false)
            {
                @object = default;
                return false;
            }

            if (_repositoryes[typeof(T2)] is T2 concrete)
            {
                @object = concrete;
                return true;
            }

            @object = default;
            return false;
        }

        public void Remove<T2>() where T2 : T1
        {
            if (_repositoryes.ContainsKey(typeof(T2)) == false)
                throw new InvalidOperationException();

            if (_repositoryes[typeof(T2)] is T2 concrete)
            {
                _repositoryes.Remove(typeof(T2));
                return;
            }

            throw new InvalidOperationException(nameof(T2));
        }

        public void Add<T2>(T2 @object) where T2 : T1
        {
            if (_repositoryes.ContainsKey(typeof(T2)))
                throw new InvalidOperationException();

            _repositoryes[typeof(T2)] = @object;
        }

        public IEnumerable<T1> GetAll()
        {
            //TODO переделать на линку
            List<T1> items = new List<T1>();

            foreach (KeyValuePair<Type, T1> repository in _repositoryes)
            {
                items.Add(repository.Value);
            }

            Debug.Log("GetCollection");
            return items;
        }

        public void AddCollection(IEnumerable<T1> items)
        {
            foreach (T1 item in items)
            {
                Type type = item.GetType();

                if (_repositoryes.ContainsKey(type))
                    throw new InvalidOperationException();

                _repositoryes[type] = item;
            }
            
            Debug.Log("AddCollection");
        }
    }
}