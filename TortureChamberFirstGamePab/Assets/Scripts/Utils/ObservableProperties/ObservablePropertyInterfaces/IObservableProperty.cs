using System;

namespace Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces
{
    public interface IObservableProperty
    {
        event Action Changed;

        string StringValue { get; }
    }
}