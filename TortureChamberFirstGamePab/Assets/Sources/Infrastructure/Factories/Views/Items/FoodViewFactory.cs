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
    public class FoodViewFactory : IItemViewFactory
    {
        private const string ItemViewPath = "Prefabs/ItemViews/";
        
        private readonly IPrefabFactory _prefabFactory;
        
        public FoodViewFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
        }
        
        //TODO не  инстанциирует объект
        public IItemView Create(IItem item) 
        {
            string fullPath = ItemViewPath + item.GetType().Name + "View";
            
            FoodView itemView = _prefabFactory.Create<FoodView>(fullPath);

            return itemView;
        }
    }
}