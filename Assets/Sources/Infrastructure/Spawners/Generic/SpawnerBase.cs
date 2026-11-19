using System;
using Infrastructure.Services.ObjectPools;
using InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using InfrastructureInterfaces.Spawners;
using Presentation.Views;
using PresentationInterfaces.Views;

namespace Infrastructure.Spawners.Generic
{
    public abstract class SpawnerBase<TViewInterface, TView, TModel> : ISpawner<TViewInterface>
        where TView : View, TViewInterface
        where TViewInterface : IView
        where TModel : new()
    {
        private readonly ObjectPool<TView> _objectPool;
        private readonly IViewFactory<TViewInterface, TView, TModel> _viewFactory;

        protected SpawnerBase(
            IViewFactory<TViewInterface, TView, TModel> viewFactory,
            ObjectPool<TView> objectPool)
        {
            _viewFactory = viewFactory ??
                           throw new ArgumentNullException(nameof(viewFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public TViewInterface Spawn()
        {
            var model = new TModel();

            return CreateFromPool(model) ?? _viewFactory.Create(model);
        }

        private TView CreateFromPool(TModel model)
        {
            var coinView = _objectPool.Get<TView>();

            if (coinView == null)
                return null;

            return (TView)_viewFactory.Create(model, coinView);
        }
    }
}