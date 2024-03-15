using Scripts.PresentationInterfaces.Views;

namespace Scripts.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic
{
    public interface IViewFactory<TView> : IViewFactory
        where TView : IView
    {
        TView Create();
        TView Create(TView view);
    }
}