using System;
using Scripts.Controllers.Items;
using Scripts.Presentation.Triggers.Items;
using Scripts.Presentation.UI.AudioSources;
using Scripts.Presentation.Views.ObjectPolls;
using Scripts.PresentationInterfaces.Views.Items.Garbages;
using Scripts.PresentationInterfaces.Views.Players;
using Scripts.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Scripts.Presentation.Views.Items.Garbages
{
    public class GarbageView : PresentableView<GarbagePresenter>, IGarbageView
    {
        private GarbageTrigger _garbageTrigger;

        [field: SerializeField] public AudioSourceUI AudioSourceUI { get; private set; }
        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;

        public IEatPointView EatPointView => Presenter.EatPointView;

        private void Awake() =>
            _garbageTrigger = GetComponentInChildren<GarbageTrigger>() ??
                              throw new NullReferenceException(nameof(GarbageTrigger));

        public void SetEatPointView(IEatPointView eatPointView) =>
            Presenter.SetEatPointView(eatPointView);

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public override void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
            Hide();
        }

        protected override void OnAfterEnable()
        {
            _garbageTrigger.Entered += OnEntered;
            _garbageTrigger.Exited += OnExited;
        }

        protected override void OnAfterDisable()
        {
            _garbageTrigger.Entered -= OnEntered;
            _garbageTrigger.Exited -= OnExited;
        }

        private void OnEntered(IPlayerMovementView playerMovementView) =>
            Presenter.CleanUpAsync();

        private void OnExited(IPlayerMovementView playerMovementView) =>
            Presenter.Cancel();
    }
}