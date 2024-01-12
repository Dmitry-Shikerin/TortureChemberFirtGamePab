using System;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Taverns;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns;

namespace Sources.Controllers.Taverns
{
    //TODO у этого презентера пустая вьюшка только для инициализации
    //TODO мб удалить эту вьюшку, сделать модель и передавать значения яв модель
    //TODO а текст юай подпишется на эту модель?
    public class TavernMoodPresenter : PresenterBase
    {
        private readonly TavernMood _tavernMood;
        private readonly ITavernMoodView _tavernMoodView;
        private readonly IImageUI _imageUI;

        public TavernMoodPresenter(TavernMood tavernMood, ITavernMoodView tavernMoodView, IImageUI imageUI)
        {
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _tavernMoodView = tavernMoodView ?? throw new ArgumentNullException(nameof(tavernMoodView));
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
            
            _imageUI.SetFillAmount(0.5f);
        }

        public override void Enable()
        {
            _tavernMood.TavernMoodValue.Changed += OnTavernMoodValueChanged;
        }

        public override void Disable()
        {
            _tavernMood.TavernMoodValue.Changed -= OnTavernMoodValueChanged;
        }

        private void OnTavernMoodValueChanged()
        {
            _imageUI.SetFillAmount(_tavernMood.TavernMoodValue.GetValue);
        }
    }
}