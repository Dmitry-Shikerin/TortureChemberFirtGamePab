using Agava.WebUtility;
using Agava.YandexGames;
using Scripts.InfrastructureInterfaces.Services.SDCServices;

namespace Scripts.Infrastructure.Services.YandexSDCServices
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