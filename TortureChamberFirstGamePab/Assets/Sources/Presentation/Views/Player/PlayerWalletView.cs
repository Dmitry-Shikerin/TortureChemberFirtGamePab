using MyProject.Sources.Controllers;

namespace MyProject.Sources.Presentation.Views
{
    public class PlayerWalletView : PresentableView<PlayerWalletPresenter>, IPlayerWalletView
    {
        public void Add(int quantity)
        {
            Presenter.Add(quantity);
        }

        public void Remove(int quantity)
        {
            Presenter.Remove(quantity);
        }
    }
}