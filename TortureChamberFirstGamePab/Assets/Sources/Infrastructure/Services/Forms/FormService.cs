using System;
using System.Collections.Generic;
using System.Linq;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Views.Forms;

namespace Sources.Infrastructure.Services.Forms
{
    public class FormService : IFormService
    {
        private readonly ContainerView _containerView;
        private Dictionary<string, IForm> _forms = new Dictionary<string, IForm>();

        public FormService(ContainerView containerView)
        {
            _containerView = containerView 
                ? containerView 
                : throw new ArgumentNullException(nameof(containerView));
        }

        //TODO что здесь значит этот оператор вопроса
        public void Add(IForm formView, string? name = null, bool isSetParent = false)
        {
            if(isSetParent)
                _containerView.AppendChild(formView);
            
            _forms.Add(name ?? formView.Name, formView);
            formView.Hide();
        }
        
        public void Show<T>() where T : IFormView => 
            Show(typeof(T).Name);

        //TODO в каких случаях доступен форич в линке?
        public void Show(string name)
        {
            if (_forms.ContainsKey(name) == false)
                throw new NullReferenceException(nameof(name));

            IForm activeForm = _forms[name];
            
            _forms.Values
                .Except(new List<IForm>() {activeForm})
                .ToList()
                .ForEach(form => form.Hide());
            
            activeForm.Show();
        }

        public void Hide<T>() where T : IFormView
        {
            string name = typeof(T).Name;

            if (_forms.ContainsKey(name) == false)
                throw new NullReferenceException(nameof(name));

            IForm activeForm = _forms[name];

            if (activeForm == null)
                throw new NullReferenceException(nameof(activeForm));
            
            activeForm.Hide();
        }
    }
}