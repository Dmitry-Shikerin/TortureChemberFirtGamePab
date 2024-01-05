using System.Threading;
using Sources.Controllers.Items;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Garbages;
using UnityEngine;

namespace Sources.Presentation.Views.Items.Garbages
{
    public class GarbageView : PresentableView<GarbagePresenter>, IGarbageView
    {
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

        public void Destroy()
        {
            //TODO выключать обьект когда он вернулся в пул
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
        }

        public void CleanUp(CancellationToken cancellationToken)
        {
            Presenter.CleanUp(cancellationToken);
        }
    }
}