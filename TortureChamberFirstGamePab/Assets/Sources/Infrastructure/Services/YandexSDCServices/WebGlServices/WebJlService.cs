using Agava.WebUtility;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;

namespace Sources.Infrastructure.Services.YandexSDCServices.WebGlServices
{
    public class WebGlService : IWebGlService
    {
        public bool IsWebGl {get; private set; }
  
        public void Enter(object payload = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
             IsWebGL = true;
#endif
        }
    }
}