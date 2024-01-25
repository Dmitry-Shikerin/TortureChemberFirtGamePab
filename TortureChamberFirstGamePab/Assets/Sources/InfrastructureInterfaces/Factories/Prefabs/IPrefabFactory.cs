using System;
using Sources.InfrastructureInterfaces.Factories.Views;
using Object = UnityEngine.Object;

namespace Sources.InfrastructureInterfaces.Factories.Prefabs
{
    public interface IPrefabFactory : IViewFactory
    {
        T Create<T>(Type viewType, string prefabPath = "") where T : Object;
    }
}