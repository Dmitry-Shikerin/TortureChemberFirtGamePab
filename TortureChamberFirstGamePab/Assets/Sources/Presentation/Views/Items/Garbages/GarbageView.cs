using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Items;
using Sources.Presentation.Triggers.Items;
using Sources.Presentation.UI.AudioSources;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Sources.Presentation.Views.Items.Garbages
{
    public class GarbageView : PresentableView<GarbagePresenter>, IGarbageView
    {
        private GarbageTrigger _garbageTrigger;

        private void Awake()
        {
            _garbageTrigger = GetComponentInChildren<GarbageTrigger>() ??
                              throw new NullReferenceException(nameof(GarbageTrigger));
        }

        [field: SerializeField] public AudioSourceUI AudioSourceUI { get; private set; }
        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;

        public IEatPointView EatPointView => Presenter.EatPointView;

        public void SetEatPointView(IEatPointView eatPointView)
        {
            Presenter.SetEatPointView(eatPointView);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

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

        private void OnEntered(IPlayerMovementView playerMovementView)
        {
            Presenter.CleanUpAsync();
        }

        private void OnExited(IPlayerMovementView playerMovementView)
        {
            Presenter.Cancel();
        }
    }
}