namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic
{
    public interface IObservableProperty<T> : IObservableProperty
    {
        public T GetValue { get; }
    }
}