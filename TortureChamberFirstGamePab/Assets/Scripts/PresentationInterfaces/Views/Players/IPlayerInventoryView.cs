using System.Collections.Generic;
using Scripts.Presentation.Views.Player.Inventory;

namespace Scripts.PresentationInterfaces.Views.Players
{
    public interface IPlayerInventoryView
    {
        float FillingRate { get; }
        IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots { get; }
    }
}