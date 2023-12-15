using UnityEngine;
using UnityEngine.UI;

namespace Sources.DomainInterfaces.Items
{
    public interface IItem
    {
        Sprite Icon { get; }
        string Title { get; }
        int Price { get; }
        float WaitingTime { get; }
    }
}