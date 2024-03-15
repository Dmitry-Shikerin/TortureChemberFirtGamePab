using System;
using Scripts.Controllers.UI;
using Scripts.PresentationInterfaces.UI;

namespace Scripts.Infrastructure.Factories.Controllers.UI
{
    public class ImageUIPresenterFactory
    {
        public ImageUIPresenter Create(IImageUI imageUI)
        {
            if (imageUI == null)
                throw new ArgumentNullException(nameof(imageUI));

            return new ImageUIPresenter(imageUI);
        }
    }
}