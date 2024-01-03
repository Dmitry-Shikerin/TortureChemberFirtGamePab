namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic
{
    public interface IObservableProperty<T> : IObservableProperty
    {
        //TODO потом исправить
        public T GetValue { get; }
    }
}