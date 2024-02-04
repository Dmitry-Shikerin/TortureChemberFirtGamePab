using Sources.PresentationInterfaces.Views.Forms;

namespace Sources.InfrastructureInterfaces.Factories.Controllers.Forms
{
    public interface IFormPresenterFactory<TFormPresenter>
    {
        TFormPresenter Create(IFormView formView);
    }
}