using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;

namespace Sources.InfrastructureInterfaces.Factories.Views.Items
{
    public interface IItemViewFactory
    {
        IItemView Create(IItem item);
    }
}