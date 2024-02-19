using System;
using Agava.YandexGames;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class PlayerAccountAuthorizeService : IPlayerAccountAuthorizeService
    {
        private readonly IWebGlService _webGlService;

        public PlayerAccountAuthorizeService(IWebGlService webGlService)
        {
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
        }

        //TODO будет ли это работать?
        //TODO вопрос славе
        public bool IsAuthorized()
        {
            if (_webGlService.IsWebGl == false)
                return false;
            
            PlayerAccount.Authorize();
                
            if(PlayerAccount.IsAuthorized)
                PlayerAccount.RequestPersonalProfileDataPermission();

            if (PlayerAccount.IsAuthorized == false)
                return false;

            return true;
        }
    }
}