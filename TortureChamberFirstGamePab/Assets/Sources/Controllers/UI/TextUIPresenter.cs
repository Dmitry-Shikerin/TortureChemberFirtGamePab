using System;
using Sources.PresentationInterfaces.UI;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;

namespace Sources.Controllers.UI
{
    public class TextUIPresenter : PresenterBase
    {
        private readonly IObservableProperty _observableProperty;
        private readonly ITextUI _textUI;

        public TextUIPresenter(
            ITextUI textUI,
            IObservableProperty observableProperty)
        {
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _observableProperty = observableProperty ??
                                  throw new ArgumentNullException(nameof(observableProperty));
        }

        public override void Enable()
        {
            _textUI.SetText(_observableProperty.StringValue);

            _observableProperty.Changed += OnPropertyChanged;
        }

        public override void Disable()
        {
            _observableProperty.Changed -= OnPropertyChanged;
        }

        private void OnPropertyChanged()
        {
            _textUI.SetText(_observableProperty.StringValue);
        }
    }
}