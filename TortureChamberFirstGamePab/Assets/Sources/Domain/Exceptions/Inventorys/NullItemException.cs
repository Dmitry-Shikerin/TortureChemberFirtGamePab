using System;

namespace Sources.Domain.Exceptions.Inventorys
{
    [Serializable]
    public class NullItemException : Exception
    {
        public NullItemException()
        {
        }

        public NullItemException(string message)
            : base(message)
        {
        }

        public NullItemException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public NullItemException(string message, string inventoryTitle)
            : this(message)
        {
            InventoryTitle = inventoryTitle;
        }

        public string InventoryTitle { get; }
    }
}