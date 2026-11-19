using System.Collections.Generic;

namespace Utils.Repositories.CollectionRepository.ContainersInterfaces
{
    public interface ICollectionContainerGeneric<T> : ICollectionContainer
    {
        IEnumerable<T> Get();
        void Set(IEnumerable<T> objects);
    }
}