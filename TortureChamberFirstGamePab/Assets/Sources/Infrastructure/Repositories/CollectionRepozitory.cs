using System;
using System.Collections.Generic;
using Sources.Infrastructure.Repositories.Containers;
using Sources.InfrastructureInterfaces.Repositoryes;
using Sources.InfrastructureInterfaces.Repositoryes.Containers;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;

namespace Sources.Infrastructure.Services
{
    //TODO как это обобщить?
    //TODO нормально ли получилось?
    public class CollectionRepozitory
    {
        private Dictionary<Type, ICollectionContainer> _repositoryes = 
            new Dictionary<Type, ICollectionContainer>();

        public List<T> Get<T>()
        {
            if (_repositoryes.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException();

            //TODO как убрать этот каст?
            if (_repositoryes[typeof(T)] is CollectionContainerGeneric<T> concrete)
                return concrete.Get();

            throw new InvalidOperationException(nameof(T));
        }

        public void Add<T>(List<T> objects)
        {
            if (_repositoryes.ContainsKey(typeof(T)))
                throw new InvalidOperationException();

            CollectionContainerGeneric<T> containerGenericCollection = new CollectionContainerGeneric<T>();
            containerGenericCollection.Set(objects);
            
            _repositoryes[typeof(T)] = containerGenericCollection;
        }
    }
}