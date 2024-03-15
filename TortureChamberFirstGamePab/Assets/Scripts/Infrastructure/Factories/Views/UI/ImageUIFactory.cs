using System;
using Scripts.Controllers.UI;
using Scripts.Infrastructure.Factories.Controllers.UI;
using Scripts.Presentation.UI;
using Scripts.PresentationInterfaces.UI;

namespace Scripts.Infrastructure.Factories.Views.UI
{
    public class ImageUIFactory
    {
        private readonly ImageUIPresenterFactory _imageUIPresenterFactory;

        public ImageUIFactory(ImageUIPresenterFactory imageUIPresenterFactory)
        {
            _imageUIPresenterFactory = imageUIPresenterFactory ??
                                       throw new ArgumentNullException(nameof(imageUIPresenterFactory));
        }

        public IImageUI Create(ImageUI imageUI)
        {
            if (imageUI == null)
                throw new ArgumentNullException(nameof(imageUI));

            ImageUIPresenter imageUIPresenter = _imageUIPresenterFactory.Create(imageUI);

            imageUI.Construct(imageUIPresenter);

            return imageUI;
        }
    }
}