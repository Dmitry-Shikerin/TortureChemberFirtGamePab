using System.Collections.Generic;
using Sources.Utils.Repositoryes.ContainersInterfaces;

namespace Sources.Utils.Repositoryes.CollectionRepository.ContainersInterfaces
{
    public interface ICollectionContainerGeneric<T> : ICollectionContainer
    {
        IEnumerable<T> Get();
        void Set(IEnumerable<T> objects);
    }
}