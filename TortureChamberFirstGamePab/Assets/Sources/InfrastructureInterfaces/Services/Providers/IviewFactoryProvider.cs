using Sources.InfrastructureInterfaces.Factories;
using Sources.PresentationInterfaces.Views;

namespace Sources.InfrastructureInterfaces.Services.Providers
{
    public interface IViewFactoryProvider
    {
        T Get<T>()
            where T : IFactory<IView>;
    }
}