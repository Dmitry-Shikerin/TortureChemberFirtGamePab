namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic
{
    public interface IObservablePropertySecond<out T> : IObservablePropertySecond
    {
        T Value { get; }

        T GetValue();

        object IObservablePropertySecond.GetValue() => 
            GetValue();
    }
}