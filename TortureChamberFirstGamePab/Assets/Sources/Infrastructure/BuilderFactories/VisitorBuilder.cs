using System;
using JetBrains.Annotations;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Infrastructure.Factorys.Views;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Views.Visitors.Inventorys;
using Sources.Utils.Repositoryes;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.BuilderFactories
{
    public class VisitorBuilder
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly ItemRepository<IItem> _itemRepository;

        public VisitorBuilder(CollectionRepository collectionRepository, ItemRepository<IItem> itemRepository)
        {
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }
        
        public void Create(ImageUIFactory imageUIFactory)
        {
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));
            
            VisitorInventoryView visitorInventoryView = Object.FindObjectOfType<VisitorInventoryView>();
            VisitorInventory visitorInventory = new VisitorInventory();
            VisitorInventoryPresenterFactory visitorInventoryPresenterFactory = 
                new VisitorInventoryPresenterFactory();
            VisitorInventoryViewFactory visitorInventoryViewFactory = 
                new VisitorInventoryViewFactory(visitorInventoryPresenterFactory);
            visitorInventoryViewFactory.Create(visitorInventoryView, visitorInventory);
            
            VisitorView visitorView = Object.FindObjectOfType<VisitorView>();
            Visitor visitor = new Visitor();
            VisitorAnimation visitorAnimation = visitorView.gameObject.GetComponent<VisitorAnimation>();
            VisitorImageUI visitorImageUI =
                visitorView.gameObject.GetComponentInChildren<VisitorImageUI>();
            VisitorPresenterFactory visitorPresenterFactory = new VisitorPresenterFactory(
                _collectionRepository);
            VisitorViewFactory visitorViewFactory = new VisitorViewFactory(
                visitorPresenterFactory);
            visitorViewFactory.Create(visitorView, visitorAnimation, visitor,
                _itemRepository, visitorImageUI, visitorInventory, imageUIFactory);

        }
    }
}