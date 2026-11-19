using Agava.WebUtility;
using Agava.YandexGames;
using InfrastructureInterfaces.Services.SDCServices;

namespace Infrastructure.Services.YandexSDCServices
{
    public class StickyService : IStickyService
    {
        public void ShowSticky()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            StickyAd.Show();
        }
    }
}