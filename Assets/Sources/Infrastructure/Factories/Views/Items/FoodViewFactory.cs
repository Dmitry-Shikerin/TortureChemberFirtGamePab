using System;
using Domain.Constants;
using DomainInterfaces.Items;
using InfrastructureInterfaces.Factories.Prefabs;
using InfrastructureInterfaces.Factories.Views.Items;
using Presentation.Views.Items.Common;
using PresentationInterfaces.Views.Items;

namespace Infrastructure.Factories.Views.Items
{
    public class FoodViewFactory : IItemViewFactory
    {
        private readonly IPrefabFactory _prefabFactory;

        public FoodViewFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
        }

        public IItemView Create(IItem item)
        {
            string fullPath = PrefabPath.ItemView + item.GetType().Name + "View";

            FoodView itemView = _prefabFactory.Create<FoodView>(fullPath);

            return itemView;
        }
    }
}