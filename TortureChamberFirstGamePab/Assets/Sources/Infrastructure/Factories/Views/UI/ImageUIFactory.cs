﻿using System;
using JetBrains.Annotations;
using Sources.Controllers.UI;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.UI;
using UnityEngine.UIElements;

namespace Sources.Infrastructure.Factories.Views.UI
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