using System;
using System.Collections.Generic;
using Sources.Utils.Repositoryes.Containers;
using Sources.Utils.Repositoryes.ContainersInterfaces;

namespace Sources.Utils.Repositoryes
{
    public class ItemRepository<T1>

    {
    private Dictionary<Type, T1> _repositoryes =
        new Dictionary<Type, T1>();

    public T1 Get<T2>() where T2 : T1
    {
        if (_repositoryes.ContainsKey(typeof(T1)) == false)
            throw new InvalidOperationException();

        //TODO как убрать этот каст?
        if (_repositoryes[typeof(T1)] is T2 concrete)
            return concrete;

        throw new InvalidOperationException(nameof(T2));
    }

    //TODO помоему дженерик здесь не нужен
    public void Add<T2>(T2 @object) where T2 : T1
    {
        if (_repositoryes.ContainsKey(typeof(T2)))
            throw new InvalidOperationException();
        
        _repositoryes[typeof(T2)] = @object;
    }

    }
}