using Sources.Infrastructure.Services.Providers;

namespace Sources.InfrastructureInterfaces.Repositories
{
    public interface IStorable
    {
        void Load(IViewFactoryProvider provider);
        void Save();
    }
}