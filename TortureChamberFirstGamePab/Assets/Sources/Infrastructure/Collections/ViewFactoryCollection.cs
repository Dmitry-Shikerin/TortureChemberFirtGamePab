using System;
using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Factories;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Collections
{
    public class ViewFactoryCollection : IViewFactoryProvider
    {
        private readonly Dictionary<Type, object> _factories = new();

        public T Get<T>()
            where T : IFactory<IView>
        {
            return (T)_factories[typeof(T)];
        }

        public void Register<T>(IFactory<T> factory)
            where T : IView
        {
            _factories[factory.GetType()] = factory ?? throw new ArgumentNullException(nameof(factory));
        }
    }
}