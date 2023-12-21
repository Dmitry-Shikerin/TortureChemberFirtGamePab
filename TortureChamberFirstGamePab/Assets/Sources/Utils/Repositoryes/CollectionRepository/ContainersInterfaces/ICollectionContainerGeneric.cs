using System.Collections.Generic;

namespace Sources.Utils.Repositoryes.ContainersInterfaces
{
    public interface ICollectionContainerGeneric<T> : ICollectionContainer
    {
        List<T> Get();
        void Set(List<T> objects);
    }
}