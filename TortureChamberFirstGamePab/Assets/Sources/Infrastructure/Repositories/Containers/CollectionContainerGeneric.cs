using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Repositoryes.Containers;

namespace Sources.Infrastructure.Repositories.Containers
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