﻿using Sources.Domain.Items.Coins;
using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.InfrastructureInterfaces.Factories.Views.Generic.Triple;
using Sources.Presentation.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Items.Garbages;

namespace Sources.Infrastructure.Spawners.Generic
{
    public class GarbageSpawner : SpawnerBase<IGarbageView, GarbageView, Garbage>
    {
        public GarbageSpawner
        (
            IViewFactory<IGarbageView, GarbageView, Garbage> viewFactory,
            ObjectPool<GarbageView> objectPool
        ) : base(viewFactory, objectPool)
        {
        }

        protected override Garbage CreateModel()
        {
            return new Garbage();
        }
    }
}