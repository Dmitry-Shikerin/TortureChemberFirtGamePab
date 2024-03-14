using System;
using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.PresentationInterfaces.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Prefabs
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly Dictionary<string, Object> _resources = new();

        public T Create<T>(string prefabPath = "")
            where T : Object, IView
        {
            try
            {
                var @object = Object.Instantiate((T)GetResource(prefabPath, typeof(T)));

                return @object;
            }
            catch (ArgumentException)
            {
                throw new NullReferenceException($"Type: {typeof(T)}, prefabPath: {prefabPath}");
            }
        }

        public T Create<T>(Type viewType, string prefabPath = "")
            where T : Object
        {
            return Object.Instantiate((T)GetResource(prefabPath, viewType));
        }

        private Object GetResource(string prefabPath, Type type)
        {
            if (_resources.ContainsKey(prefabPath) == false)
            {
                var resource = Resources.Load(prefabPath, type);
                _resources[prefabPath] = resource;
            }

            return _resources[prefabPath];
        }
    }
}