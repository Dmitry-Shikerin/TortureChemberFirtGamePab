using Sources.DomainInterfaces.Items;

namespace Sources.PresentationInterfaces.Views.Interactions.Get
{
    public interface IGettable
    {
        IItem GetTargetItem();
        void Add(IItem item);
        IItem GetItem();
    }
}