using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Sources.Domain.PlayerCameras;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Domain.PlayerMovement.PlayerMovementCharacteristics;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Views;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Animations;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factorys;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Infrastructure.Factorys.Views;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.App.Botstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string PlayerMovementCharacteristicsPath = "Configs/PlayerMovementCharacteristics";

        [SerializeField] private Transform _cameraMain;

        //TODO сделать через инстантиэйт
        [SerializeField] private InputService _inputService;
        [SerializeField] private GameObject _player;
        [SerializeField] private PlayerCameraView _playerCameraView;
        [SerializeField] private VisitorView _visitorView;

        private void Awake()
        {
            //RootGamePoints
            RootGamePoints rootGamePoints = FindObjectOfType<RootGamePoints>();
            
            //VisitorPointRepository
            VisitorPointRepositoryFactory visitorPointRepositoryFactory =
                new VisitorPointRepositoryFactory(rootGamePoints);
            CollectionRepository collectionRepository = visitorPointRepositoryFactory.Create();
            
            //ItemRepository
            ItemRepository<IItem> itemRepository = new ItemRepository<IItem>();
            
            //ItemFactory
            ItemsFactory itemsFactory = new ItemsFactory(itemRepository);
            itemsFactory.Create();

            //Visitor
            Visitor visitor = new Visitor();
            VisitorAnimation visitorAnimation = _visitorView.gameObject.GetComponent<VisitorAnimation>();
            VisitorImageUIView visitorImageUIView =
                _visitorView.gameObject.GetComponentInChildren<VisitorImageUIView>();
            VisitorPresenterFactory visitorPresenterFactory = new VisitorPresenterFactory(
                collectionRepository);
            VisitorViewFactory visitorViewFactory = new VisitorViewFactory(
                visitorPresenterFactory);
            visitorViewFactory.Create(_visitorView, visitorAnimation, visitor,
                itemRepository, visitorImageUIView);


            //PlayerCamera
            PlayerCamera playerCamera = new PlayerCamera(_cameraMain);

            PlayerCameraPresenterFactory playerCameraPresenterFactory =
                new PlayerCameraPresenterFactory(_inputService);

            PlayerCameraViewFactory playerCameraViewFactory =
                new PlayerCameraViewFactory(playerCameraPresenterFactory);

            playerCameraViewFactory.Create(_playerCameraView, playerCamera);
            _playerCameraView.SetTransform(_player.transform);

            //PlayerMovement
            IPlayerAnimation playerAnimation = _player.GetComponent<PlayerAnimation>() ??
                                               throw new NullReferenceException(nameof(PlayerAnimation));

            PlayerMovementView playerMovementView = _player.GetComponent<PlayerMovementView>();

            PlayerMovementCharacteristic playerMovementCharacteristic =
                Resources.Load<PlayerMovementCharacteristic>(PlayerMovementCharacteristicsPath);

            PlayerMovement playerMovement = new PlayerMovement(
                playerMovementCharacteristic, _cameraMain);

            PlayerMovementPresenterFactory playerMovementPresenterFactory =
                new PlayerMovementPresenterFactory(_inputService, playerAnimation);

            PlayerMovementViewFactory playerMovementViewFactory =
                new PlayerMovementViewFactory(playerMovementPresenterFactory);

            playerMovementViewFactory.Create(playerMovement, playerMovementView);
        }
    }
}