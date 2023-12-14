using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

//TODO какой это слой?
//TODO сделать его абстрактным?
public class PresentableView<T> : MonoBehaviour where T : IPresenter
{
    protected T Presenter { get; private set; }

    //TODO можно ли их сделать виртуальными?
    public void OnEnable() => 
        Presenter?.Enable();

    public void OnDisable() => 
        Presenter?.Disable();

    public void Construct(T presenter)
    {
        Hide();
        Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        Show();
    }

    private void Hide() => 
        gameObject.SetActive(false);

    private void Show() => 
        gameObject.SetActive(true);
}