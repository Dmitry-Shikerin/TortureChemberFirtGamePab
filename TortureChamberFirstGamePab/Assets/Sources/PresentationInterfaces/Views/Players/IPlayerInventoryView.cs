using System.Collections.Generic;
using Sources.Presentation.Views.Player.Inventory;
using UnityEngine;

namespace MyProject.Sources.PresentationInterfaces.Views
{
    public interface IPlayerInventoryView
    {
        float FillingRate { get; }

        public bool TryGet();
        
        IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots { get; }
    }
}