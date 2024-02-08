using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public interface IDataService<T>
    {
        bool CanLoad { get; }
        
        T Load();
        void Save(T @object);
        void Clear();
    }
}