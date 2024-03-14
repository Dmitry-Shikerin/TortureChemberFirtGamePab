using System;
using System.Collections.Generic;
using System.Linq;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Views.Forms;
using Sources.PresentationInterfaces.Views.Forms.Common;

namespace Sources.Infrastructure.Services.Forms
{
    public class FormService : IFormService
    {
        private readonly ContainerView _containerView;
        private readonly Dictionary<string, IForm> _forms = new();

        public FormService(ContainerView containerView)
        {
            _containerView = containerView
                ? containerView
                : throw new ArgumentNullException(nameof(containerView));
        }

        public void Show<T>()
            where T : IFormView
        {
            Show(typeof(T).Name);
        }

        public void Show(string name)
        {
            if (_forms.ContainsKey(name) == false)
                throw new NullReferenceException(nameof(name));

            var activeForm = _forms[name];

            _forms.Values
                .Except(new List<IForm>
                {
                    activeForm
                })
                .ToList()
                .ForEach(form => form.Hide());

            activeForm.Show();
        }

        public void Hide<T>()
            where T : IFormView
        {
            var name = typeof(T).Name;

            if (_forms.ContainsKey(name) == false)
                throw new NullReferenceException(nameof(name));

            var activeForm = _forms[name];

            if (activeForm == null)
                throw new NullReferenceException(nameof(activeForm));

            activeForm.Hide();
        }

        public void Add(IForm formView, string name = null, bool isSetParent = false)
        {
            if (isSetParent)
                _containerView.AppendChild(formView);

            _forms.Add(name ?? formView.Name, formView);
            formView.Hide();
        }
    }
}