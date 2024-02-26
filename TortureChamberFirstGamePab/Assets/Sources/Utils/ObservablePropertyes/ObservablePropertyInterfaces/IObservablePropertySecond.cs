using System;

namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces
{
    public interface IObservablePropertySecond
    {
        event Action Changed;
        
        object Value { get; }

        object GetValue();
    }
}