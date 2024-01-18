using System;
using System.Collections.Generic;
using Sources.Infrastructure.Services.Providers;
using Sources.InfrastructureInterfaces.Factories;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Collections
{
    public class ViewFactoryCollection : IViewFactoryProvider
    {
        private readonly Dictionary<Type, object> _factories = new Dictionary<Type, object>();

        public void Register<T>(IFactory<T> factory) where T : IView
        {
            _factories[factory.GetType()] = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public T Get<T>() where T : IFactory<IView> => 
            (T)_factories[typeof(T)];
    }
}