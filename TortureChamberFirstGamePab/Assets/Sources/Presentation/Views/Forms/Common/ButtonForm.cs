using System;
using Sources.ControllersInterfaces;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Views.Forms;

namespace Sources.Presentation.Views.Forms.Common
{
    public class ButtonForm<TFormView, TFormPresenter> : Form<TFormView, TFormPresenter>, IForm
        where TFormView : FormBase<TFormPresenter>
        where TFormPresenter : IPresenter
    {
        public ButtonForm
        (
            Func<TFormView, TFormPresenter> presenterFactory,
            TFormView formView,
            ButtonUIFactory buttonUIFactory,
            ButtonUI buttonUI,
            Action formAction
        ) :
            base(presenterFactory, formView)
        {
            //TODO обощить не получится потомучто нужно принимать метод конкретной вьюшки
            buttonUIFactory.Create(buttonUI, formAction);
        }

        public ButtonForm
            (
                Func<TFormView, TFormPresenter> presenterFactory,
                IPrefabFactory prefabFactory,
                ButtonUIFactory buttonUIFactory,
                ButtonUI buttonUI
                ) :
            base(presenterFactory, prefabFactory)
        {
        }
    }
}