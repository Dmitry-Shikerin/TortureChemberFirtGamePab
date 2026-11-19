using System.Collections.Generic;
using Utils.Repositories.CollectionRepository.ContainersInterfaces;

namespace Utils.Repositories.CollectionRepository.Containers
{
    public class CollectionContainerGeneric<T> : ICollectionContainerGeneric<T>
    {
        private IEnumerable<T> _objects = new List<T>();

        public IEnumerable<T> Get() =>
            _objects;

        public void Set(IEnumerable<T> objects) =>
            _objects = objects;
    }
}