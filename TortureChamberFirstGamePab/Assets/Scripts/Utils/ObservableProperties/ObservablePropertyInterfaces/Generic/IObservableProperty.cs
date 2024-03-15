namespace Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces.Generic
{
    public interface IObservableProperty<out T> : IObservableProperty
    {
        public T GetValue { get; }
    }
}