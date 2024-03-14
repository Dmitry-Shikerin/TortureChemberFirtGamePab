using System;
using Sources.Domain.Constants;
using Sources.DomainInterfaces.Items;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Factories.Views.Items;
using Sources.Presentation.Views.Items.Common;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factories.Views.Items
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
            var fullPath = Constant.PrefabPaths.ItemView + item.GetType().Name + "View";

            var itemView = _prefabFactory.Create<FoodView>(fullPath);

            return itemView;
        }
    }
}