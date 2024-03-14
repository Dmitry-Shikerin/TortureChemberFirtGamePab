using System.Collections.Generic;

namespace Sources.InfrastructureInterfaces.Repositories
{
    public interface IStorableRepository
    {
        void Add(IStorable storable);
        void Remove(IStorable storable);
        IEnumerable<IStorable> GetAll();
        void Clear();
    }
}