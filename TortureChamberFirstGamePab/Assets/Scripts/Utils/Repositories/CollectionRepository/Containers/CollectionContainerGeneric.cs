﻿using System.Collections.Generic;
using Scripts.Utils.Repositories.CollectionRepository.ContainersInterfaces;

namespace Scripts.Utils.Repositories.CollectionRepository.Containers
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