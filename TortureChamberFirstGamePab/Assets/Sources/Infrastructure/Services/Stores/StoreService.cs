using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sources.Domain.DataModels;
using Sources.Infrastructure.Services.Providers;
using Sources.InfrastructureInterfaces.DataSources;
using Sources.InfrastructureInterfaces.Repositories;
using Sources.InfrastructureInterfaces.Services.Stores;
using UnityEngine;

namespace Sources.Infrastructure.Services.Stores
{
    public class StoreService : IStoreService
    {
        private readonly IStorableRepository _storableRepository;
        private readonly IDataSource _dataSource;
        private readonly IViewFactoryProvider _viewFactoryProvider;

        public StoreService(IStorableRepository storableRepository, IDataSource dataSource,
            IViewFactoryProvider viewFactoryProvider)
        {
            _storableRepository = storableRepository ??
                                  throw new ArgumentNullException(nameof(storableRepository));
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            _viewFactoryProvider = viewFactoryProvider ?? throw new ArgumentNullException(nameof(viewFactoryProvider));
        }

        public void Load()
        {
            IEnumerable<IStorable> storables = _dataSource.Load().Select(
                dataModel =>
                {
                    Type type = Type.GetType(dataModel.Type);
                    return (IStorable)JsonConvert.DeserializeObject(dataModel.Data, type);
                });

            foreach (IStorable storable in storables)
            {
                storable.Load(_viewFactoryProvider);
                _storableRepository.Add(storable);
            }
        }

        public void Save()
        {
            IEnumerable<IStorable> storables = _storableRepository.GetAll();

            foreach (IStorable storable in storables)
            {
                storable.Save();
            }

            var dataModels = storables.Select(storable => new DataModel()
            {
                Type = storable.GetType().FullName,
                Data = JsonConvert.SerializeObject(storable)
            });
            
            _dataSource.Save(dataModels);
        }
    }
}