using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Items;
using Sources.Presentation.Triggers.Items;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Garbages;
using UnityEngine;

namespace Sources.Presentation.Views.Items.Garbages
{
    public class GarbageView : PresentableView<GarbagePresenter>, IGarbageView
    {
        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;

        private GarbageTrigger _garbageTrigger;
        public IEatPointView EatPointView => Presenter.EatPointView;

        private void Awake()
        {
            _garbageTrigger = GetComponentInChildren<GarbageTrigger>() ??
                              throw new NullReferenceException(nameof(GarbageTrigger));
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

        public void SetEatPointView(IEatPointView eatPointView) => 
            Presenter.SetEatPointView(eatPointView);

        public void SetPosition(Vector3 position) => 
            transform.position = position;

        public void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
            Hide();
        }
    }
}