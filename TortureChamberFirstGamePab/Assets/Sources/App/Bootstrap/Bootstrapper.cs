using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Domain.PlayerMovement.PlayerMovementCharacteristics;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Views;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Animations;
using Sources.App.Core;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.App;
using Sources.Infrastructure.Factorys;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Infrastructure.Factorys.Views;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Items;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.App.Botstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private AppCore _appCore;
        
        private void Awake()
        {
            _appCore = FindObjectOfType<AppCore>() ?? new AppCoreFactory().Create();
        }
    }
}