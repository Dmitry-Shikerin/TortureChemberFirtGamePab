using System;

namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces
{
    public interface IObservableProperty
    {
        event Action Changed;
        
        string StringValue { get; }
    }
}