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
        private readonly FoodViewFactory _foodViewFactory;
        
        public ItemViewFactory(IPrefabFactory prefabFactory)
        {
            _foodViewFactory = new FoodViewFactory(prefabFactory);
        }

        public IItemView Create(IItem type)
        {
            IItemView itemView = _foodViewFactory.Create(type);            

            return itemView;
        }
    }
}