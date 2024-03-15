using Scripts.DomainInterfaces.Items;

namespace Scripts.PresentationInterfaces.Views.Interactions.Get
{
    public interface ITakeble
    {
        public IItem TargetItem { get; }
        public IItem Item { get; }

        void TakeItem(IItem item);
    }
}