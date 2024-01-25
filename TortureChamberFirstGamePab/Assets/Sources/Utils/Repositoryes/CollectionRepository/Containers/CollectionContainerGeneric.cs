using System.Collections.Generic;
using Sources.Utils.Repositoryes.CollectionRepository.ContainersInterfaces;
using Sources.Utils.Repositoryes.ContainersInterfaces;

namespace Sources.Utils.Repositoryes.Containers
{
    public class CollectionContainerGeneric<T> : ICollectionContainerGeneric<T>
    {
        private List<T> _objects = new List<T>();
        
        public List<T> Get() => 
            _objects;

        public void Set(List<T> objects) => 
            _objects = objects;
    }
}