using System;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;

namespace Sources.Utils.ObservablePropertyes
{
    public class ObservableProperty<T> : IObservableProperty
    {
        private T _value;
        
        public ObservableProperty(T value = default)
        {
            _value = value;
        }
        
        public event Action Changed;

        public string StringValue => Value.ToString();

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke();
            }
        }
    }
}