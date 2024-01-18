using Sources.DomainInterfaces.Items;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
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

        public IItemView Create(IItem type) => 
            _foodViewFactory.Create(type);
    }
}