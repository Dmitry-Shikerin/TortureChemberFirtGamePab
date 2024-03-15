using System;
using Scripts.Presentation.Views;
using Scripts.Presentation.Views.ObjectPolls;

namespace Scripts.InfrastructureInterfaces.Services.ObjectPolls
{
    public interface IObjectPool
    {
        event Action<int> ObjectCountChanged;

        T Get<T>()
            where T : View;

        void Return(PoolableObject poolableObject);
    }
}