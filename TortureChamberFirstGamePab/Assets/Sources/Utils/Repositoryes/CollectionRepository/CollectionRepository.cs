using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Utils.Repositoryes.CollectionRepository.ContainersInterfaces;
using Sources.Utils.Repositoryes.Containers;

namespace Sources.Utils.Repositoryes.CollectionRepository
{
    public class CollectionRepository
    {
        private Dictionary<Type, ICollectionContainer> _repositoryes =
            new Dictionary<Type, ICollectionContainer>();

        public IReadOnlyList<T> Get<T>()
        {
            if (_repositoryes.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException();

            if (_repositoryes[typeof(T)] is not CollectionContainerGeneric<T> concrete)
                throw new InvalidOperationException(nameof(T));
            
            return concrete.Get().ToList();
        }

        public void Add<T>(IEnumerable<T> objects)
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