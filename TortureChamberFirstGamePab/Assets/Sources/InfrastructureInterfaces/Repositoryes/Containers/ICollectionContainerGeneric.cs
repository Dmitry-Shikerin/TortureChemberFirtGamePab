using System.Collections.Generic;

namespace Sources.InfrastructureInterfaces.Repositoryes.Containers
{
    public interface ICollectionContainerGeneric<T> : ICollectionContainer
    {
        List<T> Get();
        void Set(List<T> objects);
    }
}