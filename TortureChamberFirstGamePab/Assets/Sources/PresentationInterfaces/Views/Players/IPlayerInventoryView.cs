using System.Collections.Generic;
using Sources.Presentation.Views.Player.Inventory;

namespace Sources.PresentationInterfaces.Views.Players
{
    public interface IPlayerInventoryView
    {
        float FillingRate { get; }
        IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots { get; }
    }
}