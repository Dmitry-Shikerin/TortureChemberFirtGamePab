using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.Views;

namespace Scripts.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple
{
    public interface IViewFactory<out TViewInterface, in TView, in TModel> : IViewFactory
        where TView : View, TViewInterface
        where TViewInterface : IView
    {
        TViewInterface Create(TModel model);
        TViewInterface Create(TModel model, TView view);
    }
}