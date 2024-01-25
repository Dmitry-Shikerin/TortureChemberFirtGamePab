using System.Collections.Generic;
using Sources.Utils.Repositoryes.ContainersInterfaces;

namespace Sources.Utils.Repositoryes.CollectionRepository.ContainersInterfaces
{
    public interface ICollectionContainerGeneric<T> : ICollectionContainer
    {
        List<T> Get();
        void Set(List<T> objects);
    }
}