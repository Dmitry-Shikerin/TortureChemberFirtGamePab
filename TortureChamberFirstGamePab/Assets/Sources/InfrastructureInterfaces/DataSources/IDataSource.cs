using System.Collections.Generic;
using Sources.Domain.DataModels;

namespace Sources.InfrastructureInterfaces.DataSources
{
    public interface IDataSource
    {
        IEnumerable<DataModel> Load();
        void Save(IEnumerable<DataModel> models);
    }
}