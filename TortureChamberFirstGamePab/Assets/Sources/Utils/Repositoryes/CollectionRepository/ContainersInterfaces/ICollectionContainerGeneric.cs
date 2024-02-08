using System.Collections.Generic;

namespace Sources.Utils.Repositoryes.CollectionRepository.ContainersInterfaces
{
    public interface ICollectionContainerGeneric<T> : ICollectionContainer
    {
        IEnumerable<T> Get();
        void Set(IEnumerable<T> objects);
    }
}