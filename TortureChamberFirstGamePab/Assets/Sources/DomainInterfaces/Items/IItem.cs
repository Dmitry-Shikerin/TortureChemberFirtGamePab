using Sources.PresentationInterfaces.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.DomainInterfaces.Items
{
    public interface IItem
    {
        // IItemView ItemView { get; }
        Sprite Icon { get; }
        string Title { get; }
        int Price { get; }
        float WaitingTime { get; }

        IItem Clone();
    }
}