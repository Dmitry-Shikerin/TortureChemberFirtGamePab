﻿using Agava.WebUtility;
using Agava.YandexGames;
using Sources.InfrastructureInterfaces.Services.SDCServices;

namespace Sources.Infrastructure.Services.YandexSDCServices
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