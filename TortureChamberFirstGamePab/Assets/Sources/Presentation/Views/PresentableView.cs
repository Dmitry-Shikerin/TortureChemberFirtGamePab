using System;
using UnityEngine;

public class PresentableView<T> : MonoBehaviour where T : IPresenter
{
    protected T Presenter { get; private set; }

    public void OnEnable()
    {
        Presenter?.Enable();
        OnAfterEnable();
    }

    protected virtual void OnAfterEnable()
    {
    }

    public void OnDisable()
    {
        Presenter?.Disable();
        OnAfterDisable();
    }

    protected virtual void OnAfterDisable()
    {
    }

    public void Construct(T presenter)
    {
        Hide();
        Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        Show();
        OnConstructed();
    }

    protected virtual void OnConstructed()
    {
    }

    public void Hide() => 
        gameObject.SetActive(false);

    public void Show() => 
        gameObject.SetActive(true);
}