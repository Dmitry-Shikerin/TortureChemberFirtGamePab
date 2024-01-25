using System;
using Sources.PresentationInterfaces.UI;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;

namespace Sources.Controllers.UI
{
    public class TextUIPresenter : PresenterBase
    {
        private readonly ITextUI _textUI;
        private readonly IObservableProperty _observableProperty;

        public TextUIPresenter
        (
            ITextUI textUI,
            IObservableProperty observableProperty
        )
        {
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _observableProperty = observableProperty ??
                                  throw new ArgumentNullException(nameof(observableProperty));
        }

        public override void Enable() =>
            _observableProperty.Changed += OnPropertyChanged;

        public override void Disable() =>
            _observableProperty.Changed -= OnPropertyChanged;

        private void OnPropertyChanged() =>
            _textUI.SetText(_observableProperty.StringValue);
    }
}