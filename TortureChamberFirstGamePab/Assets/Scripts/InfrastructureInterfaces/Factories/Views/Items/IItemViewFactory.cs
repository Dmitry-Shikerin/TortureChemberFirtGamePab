using Scripts.DomainInterfaces.Items;
using Scripts.PresentationInterfaces.Views.Items;

namespace Scripts.InfrastructureInterfaces.Factories.Views.Items
{
    public interface IItemViewFactory
    {
        IItemView Create(IItem item);
    }
}