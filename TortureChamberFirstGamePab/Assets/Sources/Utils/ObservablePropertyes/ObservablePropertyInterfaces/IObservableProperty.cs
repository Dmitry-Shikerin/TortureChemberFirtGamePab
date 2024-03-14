using System;

namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces
{
    public interface IObservableProperty
    {
        string StringValue { get; }
        event Action Changed;
    }
}