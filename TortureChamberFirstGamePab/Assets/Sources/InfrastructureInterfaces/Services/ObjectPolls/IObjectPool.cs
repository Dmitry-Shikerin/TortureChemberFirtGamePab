using System;
using Sources.Presentation.Views.ObjectPolls;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public interface IObjectPool
    {
        event Action<int> ObjectCountChanged;
        T Get<T>() where T : MonoBehaviour;
        void Return(PoolableObject poolableObject);
    }
}