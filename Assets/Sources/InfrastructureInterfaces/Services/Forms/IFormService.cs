using PresentationInterfaces.Views.Forms.Common;

namespace InfrastructureInterfaces.Services.Forms
{
    public interface IFormService
    {
        void Show<T>()
            where T : IFormView;

        void Show(string formName);

        void Hide<T>()
            where T : IFormView;
    }
}