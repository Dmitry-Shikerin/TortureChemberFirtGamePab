using UnityEngine;

namespace MyProject.Sources.PresentationInterfaces.Views
{
    public interface IPlayerInventoryView
    {
        Transform FirstSlot { get; }
        Transform SecondSlot { get; }
        Transform ThirdSlot { get; }

        void Add();
    }
}