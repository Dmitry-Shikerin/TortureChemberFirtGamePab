using System;
using JetBrains.Annotations;
using Sources.Controllers.UI;
using Sources.PresentationInterfaces.UI;

namespace Sources.Infrastructure.Factories.Controllers.UI
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