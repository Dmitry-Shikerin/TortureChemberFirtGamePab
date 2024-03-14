using System;

namespace Sources.Domain.Exceptions.Inventorys
{
    [Serializable]
    public class InventoryFullException : Exception
    {
        public InventoryFullException()
        {
        }

        public InventoryFullException(string message)
            : base(message)
        {
        }

        public InventoryFullException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InventoryFullException(string message, string inventoryTitle)
            : this(message)
        {
            InventoryTitle = inventoryTitle;
        }

        public string InventoryTitle { get; }
    }
}