using System.Threading;
using Sources.Controllers.Items;
using Sources.PresentationInterfaces.Views.Garbages;
using UnityEngine;

namespace Sources.Presentation.Views.Items.Garbages
{
    public class GarbageView : PresentableView<GarbagePresenter>, IGarbageView
    {
        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;

        public void Destroy()
        {
            //TODO сделать добавление в обжект пул
            Destroy(gameObject);
        }

        public void CleanUp(CancellationToken cancellationToken)
        {
            Presenter.CleanUp(cancellationToken);
        }
    }
}