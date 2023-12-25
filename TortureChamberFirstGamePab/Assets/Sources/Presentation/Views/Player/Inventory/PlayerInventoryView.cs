using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Presentation.Views.Player
{
    public class PlayerInventoryView : PresentableView<PlayerInventoryPresenter>, IPlayerInventoryView
    {
        [field : SerializeField] public PlayerInventorySlotView FirstSlotView { get; private set; }
        [field : SerializeField] public PlayerInventorySlotView SecondSlotView { get; private set; }
        [field : SerializeField] public PlayerInventorySlotView ThirdSlotView { get; private set; }

        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;

        private Dictionary<object, CancellationTokenSource> _cancellationTokenSources;
        
        private List<PlayerInventorySlotView> _playerInventorySlots;

        public bool TryGet()
        {
            return Presenter.TryGet();
        }

        public IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots => _playerInventorySlots;

        private void Awake()
        {
            _playerInventorySlots = new List<PlayerInventorySlotView>(3);
            _playerInventorySlots.Add(FirstSlotView);
            _playerInventorySlots.Add(SecondSlotView);
            _playerInventorySlots.Add(ThirdSlotView);

            _cancellationTokenSources = new Dictionary<object, CancellationTokenSource>();
        }
        
        //TODO сделать булку для посетителя

        public void AddItem(IItem item) => 
            Presenter.AddItem(item);

        public async UniTask<IItem> GetItem(IItem targetItem, CancellationToken cancellationToken) => 
             await Presenter.Get(targetItem, cancellationToken);
        // public async UniTask<IItem> GetItem(IItem targetItem, CancellationToken cancellationToken) => 
        //     await Presenter.Get(targetItem, cancellationToken);
    }
}