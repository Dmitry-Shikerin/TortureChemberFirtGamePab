using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factories.Views
{
    //TOdo растащить эти интерфейсы
    public interface IViewFactory
    {
    }

    public interface IViewFactory<TView> : IViewFactory where TView : IView
    {
        TView Create();
        TView Create(TView view);
    }

    public interface IViewFactory<out TViewInterface, in TView, in TModel> : IViewFactory
        where TView : View, TViewInterface
        where TViewInterface : IView
    {
        TViewInterface Create(TModel model);
        TViewInterface Create(TModel model, TView view);
    }
}