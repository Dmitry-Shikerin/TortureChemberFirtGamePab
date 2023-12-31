﻿using System;

namespace Sources.Utils.Repositoryes.Interfaces
{
    public interface IItemRepository<T1>
    {
        public int Count { get; }

        public T2 Get<T2>() where T2 : T1;

        public bool TryGetComponent<T2>(out T2 @object) where T2 : T1;

        public void Remove<T2>() where T2 : T1;

        public void Add<T2>(T2 @object) where T2 : T1;
    }
}