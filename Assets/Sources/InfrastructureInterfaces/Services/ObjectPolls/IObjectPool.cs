using System;
using Presentation.Views;
using Presentation.Views.ObjectPolls;

namespace InfrastructureInterfaces.Services.ObjectPolls
{
    public interface IObjectPool
    {
        event Action<int> ObjectCountChanged;

        T Get<T>()
            where T : View;

        void Return(PoolableObject poolableObject);
    }
}