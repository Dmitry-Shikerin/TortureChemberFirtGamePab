using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.InfrastructureInterfaces.Factorys.Prefabs
{
    public interface IPrefabFactory
    {
        T Create<T>(string prefabPath = "") where T : MonoBehaviour;
        T Create<T>(Type viewType, string prefabPath = "") where T : Object;
    }
}