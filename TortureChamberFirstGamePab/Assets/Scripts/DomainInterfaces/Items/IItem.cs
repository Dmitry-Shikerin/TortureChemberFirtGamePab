using Scripts.PresentationInterfaces.Views.Items;
using UnityEngine;

namespace Scripts.DomainInterfaces.Items
{
    public interface IItem
    {
        IItemView ItemView { get; }
        Sprite Icon { get; }
        string Title { get; }
        int Price { get; }
        float WaitingTime { get; }

        void SetItemView(IItemView itemView);
        IItem Clone();
    }
}