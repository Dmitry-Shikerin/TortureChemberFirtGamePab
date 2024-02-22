using System;

namespace Sources.InfrastructureInterfaces.Services.SDCServices
{
    public interface IPlayerAccountAuthorizeService
    {
        bool IsAuthorized();
        void Authorize(Action onSuccessCallback);
    }
}