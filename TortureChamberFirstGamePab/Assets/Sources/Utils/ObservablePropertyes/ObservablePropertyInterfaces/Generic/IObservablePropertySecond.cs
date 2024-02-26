namespace Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic
{
    public interface IObservablePropertySecond<out T> : IObservablePropertySecond
    {
        T Value { get; }

        object IObservablePropertySecond.GetValue()
        {
            return Value;
        }
    }
}