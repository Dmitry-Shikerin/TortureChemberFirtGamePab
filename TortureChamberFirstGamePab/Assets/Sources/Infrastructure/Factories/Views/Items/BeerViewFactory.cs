using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Domain.Items;
using Sources.DomainInterfaces.Items;
using Sources.InfrastructureInterfaces.Factories.Views.Items;
using Sources.InfrastructureInterfaces.Factorys.Prefabs;
using Sources.Presentation.Views.Items;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Views.Items
{
    public class BeerViewFactory : IItemViewFactory
    {
        private const string ItemViewPath = "Prefabs/ItemViews/";
        
        private readonly IPrefabFactory _prefabFactory;
        
        public BeerViewFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
        }
        
        public IItemView Create()
        {
            string fullPath = ItemViewPath + nameof(BeerView);
            
            BeerView item = _prefabFactory.Create<BeerView>(fullPath);

            return item;
        }
    }
}