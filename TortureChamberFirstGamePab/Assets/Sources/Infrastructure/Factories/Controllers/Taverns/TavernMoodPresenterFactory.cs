using Sources.Controllers.Taverns;
using Sources.Domain.Taverns;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Infrastructure.Factories.Controllers.Taverns
{
    public class TavernMoodPresenterFactory
    {
        public TavernMoodPresenter Create(TavernMood tavernMood, ITavernMoodView tavernMoodView, IImageUI imageUI)
        {
            return new TavernMoodPresenter(tavernMood, tavernMoodView, imageUI);
        }
    }
}