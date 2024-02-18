namespace Sources.InfrastructureInterfaces.Services.LoadServices.Components
{
    public interface IDataService<T>
    {
        bool CanLoad { get; }
        
        T Load();
        void Save(T @object);
        void Clear();
    }
}