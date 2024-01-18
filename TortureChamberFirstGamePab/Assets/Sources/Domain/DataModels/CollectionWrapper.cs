using System;

namespace Sources.Domain.DataModels
{
    public class CollectionWrapper<T>
    {
        public CollectionWrapper(T[] collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public T[] Collection { get; set; }
    }
}