using System;
using PresentationInterfaces.Views;
using Object = UnityEngine.Object;

namespace InfrastructureInterfaces.Factories.Prefabs
{
    public interface IPrefabFactory
    {
        T Create<T>(Type viewType, string prefabPath = "")
            where T : Object;

        T Create<T>(string prefabPath = "")
            where T : Object, IView;
    }
}