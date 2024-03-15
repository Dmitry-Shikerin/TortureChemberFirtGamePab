using System;
using Scripts.Domain.Constants;
using Scripts.DomainInterfaces.Items;
using Scripts.InfrastructureInterfaces.Factories.Prefabs;
using Scripts.InfrastructureInterfaces.Factories.Views.Items;
using Scripts.Presentation.Views.Items.Common;
using Scripts.PresentationInterfaces.Views.Items;

namespace Scripts.Infrastructure.Factories.Views.Items
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