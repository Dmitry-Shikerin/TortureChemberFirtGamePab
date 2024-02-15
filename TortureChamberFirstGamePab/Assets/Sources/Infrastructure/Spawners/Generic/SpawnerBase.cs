﻿using System;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Sources.InfrastructureInterfaces.Spawners;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Spawners.Generic
{
    public abstract class SpawnerBase<TViewInterface, TView, TModel> : ISpawner<TViewInterface>
        where TView : View, TViewInterface
        where TViewInterface : IView
    {
        private readonly IViewFactory<TViewInterface, TView, TModel> _viewFactory;
        private readonly ObjectPool<TView> _objectPool;

        protected SpawnerBase
        (
            IViewFactory<TViewInterface, TView, TModel> viewFactory,
            ObjectPool<TView> objectPool
        )
        {
            _viewFactory = viewFactory ??
                           throw new ArgumentNullException(nameof(viewFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public TViewInterface Spawn()
        {
            TModel model = CreateModel();

            return CreateFromPool(model) ?? _viewFactory.Create(model);
        }

        private TView CreateFromPool(TModel model)
        {
            TView coinView = _objectPool.Get<TView>();

            if (coinView == null)
                return null;

            //TODO пришлось скастить иначе не мог вернуть нулл
            return (TView)_viewFactory.Create(model, coinView);
        }

        //TODO вместо этого запрашивать фанк в конструктор?
        //TODO тогда придется в зенжекте делать фромИнстанс
        protected abstract TModel CreateModel();
    }
}