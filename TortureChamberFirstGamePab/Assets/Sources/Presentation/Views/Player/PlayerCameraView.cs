﻿using System;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace MyProject.Sources.Presentation.Views
{
    public class PlayerCameraView : PresentableView<PlayerCameraPresenter>, IPlayerCameraView
    {
        private Transform _targetTransform;

        public void LateUpdate()
        {
            Presenter?.Update();
        }

        //TODO по хорошему запрашивать игрока и сменить название
        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform ? targetTransform :
                throw new ArgumentNullException(nameof(targetTransform));
        }

        public void Follow()
        {
            transform.position = _targetTransform.position;
        }

        public void Rotate(float angleY)
        {
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }
}