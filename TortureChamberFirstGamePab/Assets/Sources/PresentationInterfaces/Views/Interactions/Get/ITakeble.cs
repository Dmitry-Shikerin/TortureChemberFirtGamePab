using Sources.DomainInterfaces.Items;

namespace Sources.PresentationInterfaces.Views.Interactions.Get
{
    public interface ITakeble
    {
        public IItem TargetItem { get; }
        public IItem Item { get; }
        
        void TakeItem(IItem item);
    }
}