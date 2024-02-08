using Sources.InfrastructureInterfaces.Factories;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Services.Providers
{
    public interface IViewFactoryProvider
    {
        T Get<T>() where T : IFactory<IView>;
    }
}