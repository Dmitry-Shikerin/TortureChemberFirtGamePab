using System.Collections.Generic;
using Sources.Utils.Repositoryes.CollectionRepository.ContainersInterfaces;
using Sources.Utils.Repositoryes.ContainersInterfaces;

namespace Sources.Utils.Repositoryes.Containers
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