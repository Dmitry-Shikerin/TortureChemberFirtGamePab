using System;
using Sources.ControllersInterfaces;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services.Forms;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.PresentationInterfaces.Views.Forms;
using UnityEngine;

namespace Sources.Presentation.Views.Forms.Common
{
    public class Form<TFormView, TFormPresenter> : IForm
        where TFormView : FormBase<TFormPresenter>
        where TFormPresenter : IPresenter
    {
        private readonly TFormView _formView;

        public Form(Func<TFormView, TFormPresenter> presenterFactory, TFormView formView)
        {
            _formView = formView ? formView : throw new ArgumentNullException(nameof(formView));
            
            TFormPresenter formPresenter = presenterFactory.Invoke(_formView);
        
            _formView.Construct(formPresenter);
        
            Name = _formView.GetType().Name;
        }

        public string Name { get; }

        public void Show() =>
            _formView.Show();

        public void Hide() =>
            _formView.Hide();

        public void SetParent(Transform parentTransform) =>
            _formView.SetParent(parentTransform);

        public void SetPosition(Transform position) =>
            _formView.SetPosition(position);

        public void Destroy() =>
            _formView.Destroy();
    }
}