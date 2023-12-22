using System;

namespace Sources.Utils.Exceptions
{
    [Serializable]
    public class NullItemException : Exception
    {
        public string InventoryTitle { get; }

        public NullItemException() { }

        public NullItemException(string message)
            : base(message) { }

        public NullItemException(string message, Exception inner)
            : base(message, inner) { }

        public NullItemException(string message, string inventoryTitle)
            : this(message)
        {
            InventoryTitle = inventoryTitle;
        }

    }
}