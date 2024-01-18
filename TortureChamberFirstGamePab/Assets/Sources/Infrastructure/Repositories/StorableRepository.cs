using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Repositories;

namespace Sources.Infrastructure.Repositories
{
    public class StorableRepository : IStorableRepository
    {
        private List<IStorable> _models = new List<IStorable>();
        
        public void Add(IStorable storable) => 
            _models.Add(storable);

        public void Remove(IStorable storable) => 
            _models.Remove(storable);

        public IEnumerable<IStorable> GetAll() => 
            _models;

        public void Clear() => 
            _models.Clear();
    }
}