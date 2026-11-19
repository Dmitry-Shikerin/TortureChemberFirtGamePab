using DomainInterfaces.Items;
using InfrastructureInterfaces.Factories.Prefabs;
using PresentationInterfaces.Views.Items;

namespace Infrastructure.Factories.Views.Items.Common
{
    public class ItemViewFactory
    {
        private readonly FoodViewFactory _foodViewFactory;

        public ItemViewFactory(IPrefabFactory prefabFactory)
        {
            _foodViewFactory = new FoodViewFactory(prefabFactory);
        }

        public IItemView Create(IItem type) =>
            _foodViewFactory.Create(type);
    }
}