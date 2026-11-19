using System;
using Controllers.UI;
using PresentationInterfaces.UI;

namespace Infrastructure.Factories.Controllers.UI
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