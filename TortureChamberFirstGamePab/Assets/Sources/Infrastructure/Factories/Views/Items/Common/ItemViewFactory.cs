using System;
using System.Collections.Generic;
using Sources.Domain.Items;
using Sources.DomainInterfaces.Items;
using Sources.InfrastructureInterfaces.Factories.Views.Items;
using Sources.InfrastructureInterfaces.Factorys.Prefabs;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factories.Views.Items.Common
{
    public class ItemViewFactory
    {
        private Dictionary<Type, IItemViewFactory> _factories = new Dictionary<Type, IItemViewFactory>();

        public ItemViewFactory(IPrefabFactory prefabFactory)
        {
            _factories[typeof(Beer)] = new BeerViewFactory(prefabFactory);
        }

        //TODO для удобства запрашиваю тип модели лиюо сделать тишку у класса которыый запрашиваает фабрику
        public IItemView Create(IItem type)
        {
            if (_factories.ContainsKey(type.GetType()) == false)
                throw new AggregateException(nameof(Type));
            
            IItemView item = _factories[type.GetType()].Create();

            return item;
        }
    }
}