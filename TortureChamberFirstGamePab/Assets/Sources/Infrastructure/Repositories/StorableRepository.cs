using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Repositories;

namespace Sources.Infrastructure.Repositories
{
    public class StorableRepository : IStorableRepository
    {
        private readonly List<IStorable> _models = new();

        public void Add(IStorable storable)
        {
            _models.Add(storable);
        }

        public void Remove(IStorable storable)
        {
            _models.Remove(storable);
        }

        public IEnumerable<IStorable> GetAll()
        {
            return _models;
        }

        public void Clear()
        {
            _models.Clear();
        }
    }
}