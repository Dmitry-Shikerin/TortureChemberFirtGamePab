using System;

namespace Scripts.InfrastructureInterfaces.Services.SDCServices
{
    public interface IPlayerAccountAuthorizeService
    {
        bool IsAuthorized();
        void Authorize(Action onSuccessCallback);
    }
}