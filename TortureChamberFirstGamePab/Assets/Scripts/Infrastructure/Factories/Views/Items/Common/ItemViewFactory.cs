using Scripts.DomainInterfaces.Items;
using Scripts.InfrastructureInterfaces.Factories.Prefabs;
using Scripts.PresentationInterfaces.Views.Items;

namespace Scripts.Infrastructure.Factories.Views.Items.Common
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