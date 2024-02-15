using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Views;

namespace Sources.InfrastructureInterfaces.Factories.Views.Generic.Triple
{
    public interface IViewFactory<out TViewInterface, in TView, in TModel> : IViewFactory
        where TView : View, TViewInterface
        where TViewInterface : IView
    {
        TViewInterface Create(TModel model);
        TViewInterface Create(TModel model, TView view);
    }
}