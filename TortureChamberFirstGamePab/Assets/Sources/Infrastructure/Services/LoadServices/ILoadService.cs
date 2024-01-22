using Sources.Domain.Players;

namespace Sources.Infrastructure.Services.LoadServices
{
    public interface ILoadService
    {
        void Load();
        void Save();
    }
}