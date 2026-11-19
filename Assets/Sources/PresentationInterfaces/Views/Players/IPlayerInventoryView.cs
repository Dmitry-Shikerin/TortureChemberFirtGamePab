using System.Collections.Generic;
using Presentation.Views.Player.Inventory;

namespace PresentationInterfaces.Views.Players
{
    public interface IPlayerInventoryView
    {
        float FillingRate { get; }
        IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots { get; }
    }
}