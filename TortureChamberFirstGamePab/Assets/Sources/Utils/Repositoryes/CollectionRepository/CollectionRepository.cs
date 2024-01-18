using System;
using System.Collections.Generic;
using Sources.Utils.Repositoryes.Containers;
using Sources.Utils.Repositoryes.ContainersInterfaces;

namespace Sources.Utils.Repositoryes.CollectionRepository
{
    public class CollectionRepository
    {
        private Dictionary<Type, ICollectionContainer> _repositoryes = 
            new Dictionary<Type, ICollectionContainer>();

        public List<T> Get<T>()
        {
            if (_repositoryes.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException();

            if (_repositoryes[typeof(T)] is CollectionContainerGeneric<T> concrete)
                return concrete.Get();

            throw new InvalidOperationException(nameof(T));
        }

        public void Add<T>(List<T> objects)
        {
            if (_repositoryes.ContainsKey(typeof(T)))
                throw new InvalidOperationException();

            CollectionContainerGeneric<T> containerGenericCollection = 
                new CollectionContainerGeneric<T>();
            containerGenericCollection.Set(objects);
            
            _repositoryes[typeof(T)] = containerGenericCollection;
        }
    }
}