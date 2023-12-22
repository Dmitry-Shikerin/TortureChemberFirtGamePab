using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    private void Hide() => 
        gameObject.SetActive(false);

    private void Show() => 
        gameObject.SetActive(true);
}