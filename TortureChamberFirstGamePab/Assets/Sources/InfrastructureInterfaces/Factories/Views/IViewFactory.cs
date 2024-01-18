using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factories.Views
{
    public interface IViewFactory
    {
        T Create<T>(string prefabPath = "") where T : Object, IView;
    }
}