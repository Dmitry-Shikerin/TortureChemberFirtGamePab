using Sources.Domain.Players;

namespace Sources.Infrastructure.Services.LoadServices
{
    public interface ILoadService
    {
        //TODO заменил что бы он ретернил плеера
        Player Load();
        //TODO этот сервис должен сохранять или плеер дата сервис
        void Save();
    }
}