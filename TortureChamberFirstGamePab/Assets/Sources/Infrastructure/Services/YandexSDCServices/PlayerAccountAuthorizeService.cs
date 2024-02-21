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
        //TODO сделать форму запроса авторизации
        public bool IsAuthorized()
        {
            if (_webGlService.IsWebGl == false)
                return false;

            if (PlayerAccount.IsAuthorized == false)
            {
                //TODO вызвать фомучку спросить хотите ли авторизоваться?
                //TODO если соглашается
                PlayerAccount.Authorize();
                //TODO закрываю окошко
            }
            
            // PlayerAccount.Authorize();
                
            //TODO можно не делать вызывается только один раз
            // if(PlayerAccount.IsAuthorized)
            //     PlayerAccount.RequestPersonalProfileDataPermission();

            if (PlayerAccount.IsAuthorized == false)
                return false;

            return true;
        }
    }
}