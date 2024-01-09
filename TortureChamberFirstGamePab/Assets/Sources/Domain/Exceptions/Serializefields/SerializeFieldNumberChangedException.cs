using System;

namespace Sources.Domain.Exceptions.Serializefields
{
    [Serializable]
    public class SerializeFieldNumberChangedException : Exception
    {
        public SerializeFieldNumberChangedException() { }

        public SerializeFieldNumberChangedException(string message)
            : base(message) { }

        public SerializeFieldNumberChangedException(string message, Exception inner)
            : base(message, inner) { }

        public SerializeFieldNumberChangedException(string message, string fieldTitle)
            : this(message)
        {
            FieldTitle = fieldTitle;
        }
        
        public string FieldTitle { get; }
    }
}