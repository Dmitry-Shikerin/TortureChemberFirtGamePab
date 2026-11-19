using DomainInterfaces.Items;
using PresentationInterfaces.Views.Items;

namespace InfrastructureInterfaces.Factories.Views.Items
{
    public interface IItemViewFactory
    {
        IItemView Create(IItem item);
    }
}