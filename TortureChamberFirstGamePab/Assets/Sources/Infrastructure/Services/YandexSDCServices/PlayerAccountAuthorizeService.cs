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

        //TODO пойдет ли так?
        //TODO сделать форму запроса авторизации
        public bool IsAuthorized()
        {
            //TODO пока не вебе постоянно будет показывать окошко
            if (_webGlService.IsWebGl == false)
                return false;

            if (PlayerAccount.IsAuthorized == false)
                return false;

            return true;
        }

        //TODO вызвать фомучку спросить хотите ли авторизоваться?
        //TODO если соглашается
        public void Authorize(Action onSuccessCallback)
        {            
            if (_webGlService.IsWebGl == false)
                return;

            if(PlayerAccount.IsAuthorized)
                return;
            
            PlayerAccount.Authorize();
        }
    }
}